using Godot;
using System;
using System.Collections.Generic;

public partial class MoveAbility : Ability
{
    public MoveAbility(string inputAction) : base(
        inputAction, 
        "move", 
        "Move", 
        "res://assets/move.jpg", 
        2, 
        1, 
        1,
        2
    ) { }

    public override void Execute(AbilityExecution execution)
    {
        Tile startTile = execution.WorldState.GetTileAtCoordinates(execution.Source.Coordinates);
        Tile endTile = execution.WorldState.GetTileAtCoordinates(execution.Targets[0]);

        startTile.CurrentActor = null;
        endTile.CurrentActor = execution.Source;
        execution.Source.Coordinates = execution.Targets[0];
    }
}
