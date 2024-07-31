using Godot;
using System;

public enum AbilitySelectStates
{
	None,
	ActorSelected,
	AbilitySelected,
	Confirm,
}

public partial class BattleManager : Node2D
{
	private World world;

	private TurnOrder turnOrder;
	private ElementalSpectra elementalSpectra;
	private AbilityButtons abilityButtons;

	private AbilitySelectStates abilitySelectState = AbilitySelectStates.None;
	private List<Vector2> selectedCoords = new List<Vector2>();
	private Actor selectedActor;
	private string selectedAbility;

	public override void _Ready()
	{
		world = GetNode<World>("World");
		turnOrder = GetNode<TurnOrder>("TurnOrder");
		elementalSpectra = GetNode<ElementalSpectra>("ElementalSpectra");
		abilityButtons = GetNode<AbilityButtons>("AbilityButtons");

		turnOrder.SetTurnOrder(world.Actors);
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
			abilitySelectState = AbilitySelectStates.None;
			selectedCoords = new List<Vector2>();
			selectedActor = null;
			selectedAbility = null;

			abilityButtons.HideConfirmButtons();
			world.ClearHighlightTile();
			abilityButtons.ClearAbilities();
		}
	}

	private void _onTileClicked(Vector2 coordinates)
	{
		// If we haven't selected an ability for an actor yet, we know we're not targeting for an
		// ability, we're selecting a tile
		if (
			abilitySelectState == AbilitySelectStates.None ||
			abilitySelectState == AbilitySelectStates.ActorSelected
		)
		{
			world.HighlightTile(coordinates);
			selectedActor = world.GetActorAtCoordinates(coordinates);

			// Sets or clears the ability buttons depending if an actor was on the tile selected
			if (selectedActor is not null)
			{
				abilitySelectState = AbilitySelectStates.ActorSelected;
				abilityButtons.SetAbilities(selectedActor.Abilities);
			}
			else
			{
				abilitySelectState = AbilitySelectStates.None;
				abilityButtons.ClearAbilities();
			}
		}
		else if (abilitySelectState == AbilitySelectStates.AbilitySelected)
		{
			// If an ability is selected, we're assuming we're targeting for that ability
			selectedCoords.Add(coordinates);
			evaluateNumberOfTargets();
		}
	}



	private void _onAbilityButtonClicked(string action)
	{
		if (abilitySelectState == AbilitySelectStates.ActorSelected)
		{
			string[] validKeys = { "Q", "W", "E" };
			if (validKeys.Contains(action))
			{
				abilitySelectState = AbilitySelectStates.AbilitySelected;
				selectedCoords = new List<Vector2>();
				selectedAbility = action;
				// We want to evaluate targets and show the confirm button in case the ability has
				// 0 expected targets
				evaluateNumberOfTargets();
			}
		}
	}

	private void evaluateNumberOfTargets()
	{
		// if the ability has enough targets selected, show confirmation button
		int expectedTargetCount = selectedActor
			.Abilities
			.Find(ab => ab.InputAction == selectedAbility)
			.NumberOfTargets;

		if (expectedTargetCount <= selectedCoords.Count)
		{
			abilitySelectState = AbilitySelectStates.Confirm;
			Dictionary<string, int> keyMap = new Dictionary<string, int>() { { "Q", 0 }, { "W", 1 }, { "E", 2 } };
			abilityButtons.ShowConfirmButton(keyMap[selectedAbility]);
		}
	}

	private void _onAbilityConfirmButtonClicked(string action)
	{
		// If the confirm is clicked, we want to keep the actor selected, but remove everythign else
		// world.ExecuteAbility(SelectedActor, SelectedAbility, SelectedCoords, spectrum);
		abilitySelectState = AbilitySelectStates.ActorSelected;
		abilityButtons.HideConfirmButtons();
		selectedCoords = new List<Vector2>();
		selectedAbility = null;
	}

	private void _onEndTurnButtonClicked()
	{
		turnOrder.GoToNextActor();
	}
}
