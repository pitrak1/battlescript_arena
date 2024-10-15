using Godot;
using System;

public enum ActionStates
{
	None,
	AbilitySelected,
	Confirm,
}

public partial class Battle : Control
{
	private World world;

	private TurnOrder turnOrder;
	private ElementalSpectra elementalSpectra;
	private AbilityButtons abilityButtons;

	private AbilityExecutor executor;

	private ActionStates actionState = ActionStates.None;
	private List<Vector2> selectedTargets = new List<Vector2>();
	private string selectedAction;

	private int actionPointsLeft = 8;

	public override void _Ready()
	{
		world = GetNode<World>("World");
		turnOrder = GetNode<TurnOrder>("TurnOrder");
		elementalSpectra = GetNode<ElementalSpectra>("ElementalSpectra");
		abilityButtons = GetNode<AbilityButtons>("AbilityButtons");
		executor = new AbilityExecutor(world, turnOrder, elementalSpectra);

		turnOrder.SetTurnOrder(world.Actors);
		abilityButtons.SetAbilities(turnOrder.CurrentActor.Abilities);
		world.SetCurrentTile(turnOrder.CurrentActor.Coordinates);

		turnOrder.SetActionPoints(5);
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		string[] validKeys = { "Q", "W", "E" };
		foreach (string key in validKeys)
		{
			if (Input.IsActionJustPressed(key))
			{
				_onAbilityButtonClicked(key);
			}
		}

		// right click is our "get out of everything and select nothing" button
		if (Input.IsActionJustPressed("RMB"))
		{
			actionState = ActionStates.None;
			selectedTargets = new List<Vector2>();
			selectedAction = null;

			abilityButtons.HideConfirmButtons();
			world.ClearSelectedTile();
			world.ClearTargetedTiles();
		}
	}

	private void _onTileClicked(Vector2 coordinates)
	{
		// If we haven't selected an ability yet, we know we're not targeting for an
		// ability, we're selecting a tile
		if (actionState == ActionStates.None)
		{
			world.SetSelectedTile(coordinates);
		}
		else if (actionState == ActionStates.AbilitySelected)
		{
			// If an ability is selected, we're assuming we're targeting for that ability
			selectedTargets.Add(coordinates);
			world.SetTargetedTiles(selectedTargets);
			evaluateNumberOfTargets();
		}
	}

	private void _onAbilityButtonClicked(string action)
	{
		if (actionState == ActionStates.None)
		{
			string[] validKeys = { "Q", "W", "E" };
			if (validKeys.Contains(action))
			{
				actionState = ActionStates.AbilitySelected;
				selectedTargets = new List<Vector2>();
				selectedAction = action;
				// We want to evaluate targets and show the confirm button in case the ability has
				// 0 expected targets
				evaluateNumberOfTargets();
			}
		}
	}

	private void evaluateNumberOfTargets()
	{
		// if the ability has enough targets selected, show confirmation button
		int expectedTargetCount = turnOrder
			.CurrentActor
			.Abilities
			.Find(ab => ab.InputAction == selectedAction)
			.BaseNumberOfTargets;

		if (expectedTargetCount <= selectedTargets.Count)
		{
			actionState = ActionStates.Confirm;
			Dictionary<string, int> keyMap = new Dictionary<string, int>() { { "Q", 0 }, { "W", 1 }, { "E", 2 } };
			abilityButtons.ShowConfirmButton(keyMap[selectedAction]);
		}
	}

	private void _onAbilityConfirmButtonClicked(string action)
	{
		// If the confirm is clicked, we want to keep the actor selected, but remove everythign else
		OnAbilityExecuted();
		actionState = ActionStates.None;
		abilityButtons.HideConfirmButtons();
		selectedTargets = new List<Vector2>();
		selectedAction = null;

		world.ClearTargetedTiles();
	}

	private void _onEndTurnButtonClicked()
	{
		OnTurnEnd();
		turnOrder.GoToNextActor();
		abilityButtons.SetAbilities(turnOrder.CurrentActor.Abilities);
		world.SetCurrentTile(turnOrder.CurrentActor.Coordinates);
	}

	public void OnAbilityExecuted()
	{
		Dictionary<string, int> actionMap = new Dictionary<string, int>() { { "Q", 0 }, { "W", 1 }, { "E", 2 } };
		Ability selectedAbility = turnOrder.CurrentActor.Abilities[actionMap[selectedAction]];
		executor.Execute(selectedAbility, turnOrder.CurrentActor, selectedTargets);
		world.SetCurrentTile(turnOrder.CurrentActor.Coordinates);
	}

	public void OnTurnEnd() 
	{ 
		foreach(Actor actor in world.Actors) {
			List<ActorEffect> effectsToBeRemoved = new List<ActorEffect>();

			foreach(ActorEffect effect in actor.Effects) {
				if (actor == turnOrder.CurrentActor) {
					bool remove = effect.OnActorTurnEnd(world, turnOrder, elementalSpectra);
					if (remove) { effectsToBeRemoved.Add(effect); }
				} else {
					bool remove = effect.OnTurnEnd(world, turnOrder, elementalSpectra);
					if (remove) { effectsToBeRemoved.Add(effect); }
				}
			}

			foreach(ActorEffect effectToBeRemoved in effectsToBeRemoved) {
				actor.Effects.Remove(effectToBeRemoved);
			}
		}

		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				Tile tile = world.GetTileAtCoordinates(new Vector2(x, y));
				if (tile is not null)
				{
					List<TileEffect> effectsToBeRemoved = new List<TileEffect>();

					foreach(TileEffect effect in tile.Effects) {
						bool remove = effect.OnTurnEnd(world, turnOrder, elementalSpectra);
						if (remove) { effectsToBeRemoved.Add(effect); }
					}

					foreach(TileEffect effectToBeRemoved in effectsToBeRemoved) {
						tile.Effects.Remove(effectToBeRemoved);
					}
				}
			}
		}
	}

	public void OnTurnStart() 
	{ 
		foreach(Actor actor in world.Actors) {
			List<ActorEffect> effectsToBeRemoved = new List<ActorEffect>();

			foreach(ActorEffect effect in actor.Effects) {
				if (actor == turnOrder.CurrentActor) {
					bool remove = effect.OnActorTurnStart(world, turnOrder, elementalSpectra);
					if (remove) { effectsToBeRemoved.Add(effect); }
				} else {
					bool remove = effect.OnTurnStart(world, turnOrder, elementalSpectra);
					if (remove) { effectsToBeRemoved.Add(effect); }
				}
			}

			foreach(ActorEffect effectToBeRemoved in effectsToBeRemoved) {
				actor.Effects.Remove(effectToBeRemoved);
			}
		}

		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				Tile tile = world.GetTileAtCoordinates(new Vector2(x, y));
				if (tile is not null)
				{
					List<TileEffect> effectsToBeRemoved = new List<TileEffect>();

					foreach(TileEffect effect in tile.Effects) {
						bool remove = effect.OnTurnStart(world, turnOrder, elementalSpectra);
						if (remove) { effectsToBeRemoved.Add(effect); }
					}

					foreach(TileEffect effectToBeRemoved in effectsToBeRemoved) {
						tile.Effects.Remove(effectToBeRemoved);
					}
				}
			}
		}
	}
}
