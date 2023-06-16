using Godot;
using System;
using System.Collections.Generic;

public partial class MoveAbility : Ability
{
    public MoveAbility(string key, string displayName, int usesPerTurn, int cooldown, string inputAction) : base(key, displayName, usesPerTurn, cooldown, inputAction, 1) { }

    public override bool Execute(Actor source, World world, List<Vector2> target)
    {
        Tile startTile = world.GetTile(source.Coordinates);
        Tile endTile = world.GetTile(target[0]);
        Actor actor = startTile.RemoveActor();
        endTile.PlaceActor(actor);
        return true;
    }
}
