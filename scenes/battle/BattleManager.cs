using Godot;
using System;

public enum ActionStates
{
	None,
	AbilitySelected,
	Confirm,
}

public partial class BattleManager : Node2D
{
	private World world;

	private TurnOrder turnOrder;
	private ElementalSpectra elementalSpectra;
	private AbilityButtons abilityButtons;

	private ActionStates actionState = ActionStates.None;
	private List<Vector2> selectedCoords = new List<Vector2>();
	private string selectedAction;

	public override void _Ready()
	{
		world = GetNode<World>("World");
		turnOrder = GetNode<TurnOrder>("TurnOrder");
		elementalSpectra = GetNode<ElementalSpectra>("ElementalSpectra");
		abilityButtons = GetNode<AbilityButtons>("AbilityButtons");

		turnOrder.SetTurnOrder(world.Actors);
		abilityButtons.SetAbilities(turnOrder.CurrentActor.Abilities);
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
			selectedCoords = new List<Vector2>();
			selectedAction = null;

			abilityButtons.HideConfirmButtons();
			world.ClearHighlights();
		}
	}

	private void _onTileClicked(Vector2 coordinates)
	{
		// If we haven't selected an ability yet, we know we're not targeting for an
		// ability, we're selecting a tile
		if (actionState == ActionStates.None)
		{
			world.ClearHighlights();
			world.HighlightTile(coordinates);
		}
		else if (actionState == ActionStates.AbilitySelected)
		{
			// If an ability is selected, we're assuming we're targeting for that ability
			selectedCoords.Add(coordinates);
			world.HighlightTile(coordinates);
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
				selectedCoords = new List<Vector2>();
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
			.NumberOfTargets;

		if (expectedTargetCount <= selectedCoords.Count)
		{
			actionState = ActionStates.Confirm;
			Dictionary<string, int> keyMap = new Dictionary<string, int>() { { "Q", 0 }, { "W", 1 }, { "E", 2 } };
			abilityButtons.ShowConfirmButton(keyMap[selectedAction]);
		}
	}

	private void _onAbilityConfirmButtonClicked(string action)
	{
		// If the confirm is clicked, we want to keep the actor selected, but remove everythign else
		executeAbility();
		actionState = ActionStates.None;
		abilityButtons.HideConfirmButtons();
		selectedCoords = new List<Vector2>();
		selectedAction = null;

		world.ClearHighlights();
		world.HighlightTile(turnOrder.CurrentActor.Coordinates);
	}

	private void _onEndTurnButtonClicked()
	{
		turnOrder.GoToNextActor();
		abilityButtons.SetAbilities(turnOrder.CurrentActor.Abilities);
	}

	private void executeAbility()
	{
		Dictionary<string, int> actionMap = new Dictionary<string, int>() { { "Q", 0 }, { "W", 1 }, { "E", 2 } };
		Ability selectedAbility = turnOrder.CurrentActor.Abilities[actionMap[selectedAction]];
		selectedAbility.Execute(turnOrder.CurrentActor, selectedCoords, world, turnOrder, elementalSpectra);
	}
}
