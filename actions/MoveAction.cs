using Godot;
using System;

public partial class MoveAction : Action
{
    public MoveAction(string key, string displayName, int usesPerTurn, int cooldown, string inputAction) : base(key, displayName, usesPerTurn, cooldown, inputAction) { }

    public override bool Execute(Actor source, World world, Vector2 target)
    {
        Tile startTile = world.GetTile(source.Coordinates);
        Tile endTile = world.GetTile(target);
        Actor actor = startTile.RemoveActor();
        endTile.PlaceActor(actor);
        return true;
    }
}
