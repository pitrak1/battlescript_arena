using Godot;
using System;
using System.Collections.Generic;

public partial class MoveAbility : Ability
{
    public MoveAbility(string inputAction) : base(inputAction, "move", "Move", "res://assets/move.jpg", 2, 1, 1) { }

    public override bool ExecuteAction(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra)
    {
        Tile startTile = world.GetTileAtCoordinates(source.Coordinates);
        Tile endTile = world.GetTileAtCoordinates(targets[0]);

        startTile.CurrentActor = null;
        endTile.CurrentActor = source;
        source.Coordinates = targets[0];

        return true;
    }
}
