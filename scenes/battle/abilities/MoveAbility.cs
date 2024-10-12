using Godot;
using System;
using System.Collections.Generic;

public partial class MoveAbility : Ability
{
    public MoveAbility(string inputAction) : base(inputAction) 
	{
		Key = RegisteredAbilities.Move;
		DisplayName = "Move";
		IconAsset = "res://assets/move.jpg";

		BaseUsesPerTurn = 2;
		RemainingUsesPerTurn = 2;

		BaseCooldown = 1;
		RemainingCooldown = 0;

		BaseNumberOfTargets = 1;
		BaseActionPointCost = 2;
	}

    public override void Execute(AbilityExecution execution)
    {
        // Tile startTile = execution.WorldState.GetTileAtCoordinates(execution.Source.Coordinates);
        // Tile endTile = execution.WorldState.GetTileAtCoordinates(execution.Targets[0]);

        // startTile.CurrentActor = null;
        // endTile.CurrentActor = execution.Source;
        // execution.Source.Coordinates = execution.Targets[0];
    }
}
