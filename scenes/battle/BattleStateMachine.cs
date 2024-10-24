using Godot;
using System;

public enum ActionStates
{
	None,
	AbilitySelected,
	Confirm,
}

public class BattleStateMachine
{
	private World world;
	private TurnOrder turnOrder;
	private ElementalSpectra elementalSpectra;
	private AbilityButtons abilityButtons;
	private AbilityExecutor executor;

	public ActionStates CurrentState = ActionStates.None;
	public List<Vector2> SelectedTargets = new List<Vector2>();
	public Ability SelectedAbility;
	public int SelectedAbilityIndex = -1;

	public BattleStateMachine(World _world, TurnOrder _turnOrder, ElementalSpectra _elementalSpectra, AbilityButtons _abilityButtons)
	{
		world = _world;
		turnOrder = _turnOrder;
		elementalSpectra = _elementalSpectra;
		abilityButtons = _abilityButtons;

		executor = new AbilityExecutor(world, turnOrder, elementalSpectra);

		turnOrder.SetTurnOrder(world.Actors);
		abilityButtons.SetAbilities(turnOrder.CurrentActor.Abilities);
		world.SetCurrentTile(turnOrder.CurrentActor.Coordinates);
	}

	public void HandleAbilityButtonClicked(Ability ability, int index)
	{
		if (CurrentState == ActionStates.None)
		{
			CurrentState = ActionStates.AbilitySelected;
			SelectedTargets = new List<Vector2>();
			SelectedAbility = ability;
			SelectedAbilityIndex = index;
			// We want to evaluate targets and show the confirm button in case the ability has
			// 0 expected targets
			evaluateNumberOfTargets();
		}
	}

	private void evaluateNumberOfTargets()
	{
		if (SelectedTargets.Count >= SelectedAbility.BaseNumberOfTargets)
		{
			CurrentState = ActionStates.Confirm;
			abilityButtons.ShowConfirmButton(SelectedAbilityIndex);
		}
	}

	// right click is our "get out of everything and select nothing" button
	public void HandleRightMouseButtonClicked()
	{
		CurrentState = ActionStates.None;
		SelectedTargets = new List<Vector2>();
		SelectedAbility = null;
		SelectedAbilityIndex = -1;

		abilityButtons.HideConfirmButtons();
		world.ClearSelectedTile();
		world.ClearTargetedTiles();
	}

	public void HandleTileClicked(Vector2 coordinates)
	{
		// If we haven't selected an ability yet, we know we're not targeting for an
		// ability, we're selecting a tile
		if (CurrentState == ActionStates.None)
		{
			world.SetSelectedTile(coordinates);
		}
		else if (CurrentState == ActionStates.AbilitySelected)
		{
			// If an ability is selected, we're assuming we're targeting for that ability
			SelectedTargets.Add(coordinates);
			world.SetTargetedTiles(SelectedTargets);
			evaluateNumberOfTargets();
		}
	}

	public void HandleAbilityConfirmButtonClicked()
	{
		executor.Execute(SelectedAbility, turnOrder.CurrentActor, SelectedTargets);
		
		world.SetCurrentTile(turnOrder.CurrentActor.Coordinates);
		world.ClearTargetedTiles();

		CurrentState = ActionStates.None;
		abilityButtons.HideConfirmButtons();
		SelectedAbility = null;	
		SelectedAbilityIndex = -1;
		SelectedTargets = new List<Vector2>();
	}

	public void HandleEndTurnButtonClicked()
	{
		executor.OnTurnEnd();
		turnOrder.GoToNextActor();
		abilityButtons.SetAbilities(turnOrder.CurrentActor.Abilities);
		world.SetCurrentTile(turnOrder.CurrentActor.Coordinates);
		executor.OnTurnStart();
	}
}
