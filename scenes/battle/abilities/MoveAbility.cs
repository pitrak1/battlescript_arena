using Godot;
using System;
using System.Collections.Generic;

public partial class MoveAbility : Ability
{
    public MoveAbility(string inputAction) : base(inputAction, "move", "Move", 2, 1, 1) { }

    public override bool ExecuteAction(Actor source, World world, List<Vector2> target, ElementalSpectra spectra)
    {
        // Tile startTile = world.GetTile(source.Coordinates);
        // Tile endTile = world.GetTile(target[0]);
        // Actor actor = startTile.RemoveActor();
        // endTile.PlaceActor(actor);
        return true;
    }
}
