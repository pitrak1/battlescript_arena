using Godot;
using System;
using System.Collections.Generic;

public partial class MoveAbility : Ability
{
    public MoveAbility(string inputAction) : base(inputAction, "move", "Move", 2, 0, 1) { }

    public override bool Execute(Actor source, World world, List<Vector2> target, Spectrum spectrum)
    {
        Tile startTile = world.GetTile(source.Coordinates);
        Tile endTile = world.GetTile(target[0]);
        Actor actor = startTile.RemoveActor();
        endTile.PlaceActor(actor);
        return true;
    }
}
