using Godot;
using System;

public partial class Battle : Control
{
	private World world;
	private TurnOrder turnOrder;
	private ElementalSpectra elementalSpectra;
	private AbilityButtons abilityButtons;

	private BattleStateMachine stateMachine;
	private string[] validAbilityActions = { "Q", "W", "E", "R", "T" };

	public override void _Ready()
	{
		world = GetNode<World>("WorldAndUI/World");
		turnOrder = GetNode<TurnOrder>("WorldAndUI/TurnOrder");
		elementalSpectra = GetNode<ElementalSpectra>("WorldAndUI/ElementalSpectra");
		abilityButtons = GetNode<AbilityButtons>("WorldAndUI/AbilityButtons");

		stateMachine = new BattleStateMachine(world, turnOrder, elementalSpectra, abilityButtons);
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		foreach (string action in validAbilityActions)
		{
			if (validAbilityActions.Contains(action) && Input.IsActionJustPressed(action))
			{
				Ability clickedAbility = 
					turnOrder
					.CurrentActor
					.Abilities
					.Find(ab => ab.InputAction == action);
				int index = Array.IndexOf(validAbilityActions, action);
				stateMachine.HandleAbilityButtonClicked(clickedAbility, index);
			}
		}

		if (Input.IsActionJustPressed("RMB"))
		{
			stateMachine.HandleRightMouseButtonClicked();
		}
	}

	private void _onTileClicked(Vector2 coordinates)
	{
		stateMachine.HandleTileClicked(coordinates);
	}

	private void _onAbilityButtonClicked(string action)
	{
		Ability clickedAbility = 
			turnOrder
			.CurrentActor
			.Abilities
			.Find(ab => ab.InputAction == action);
		int index = Array.IndexOf(validAbilityActions, action);
		stateMachine.HandleAbilityButtonClicked(clickedAbility, index);
	}

	private void _onAbilityConfirmButtonClicked(string action)
	{
		stateMachine.HandleAbilityConfirmButtonClicked();
	}

	private void _onEndTurnButtonClicked()
	{
		stateMachine.HandleEndTurnButtonClicked();
	}
}
