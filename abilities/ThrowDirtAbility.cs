using Godot;
using System;
using System.Collections.Generic;

public partial class ThrowDirtAbility : Ability
{
    public ThrowDirtAbility(string inputAction) : base(inputAction, "throwDirt", "Throw Dirt", 2, 0, 1) { }

    public override bool ExecuteAction(Actor source, World world, List<Vector2> target, Spectrum spectrum)
    {
        Tile startTile = world.GetTile(source.Coordinates);
        Tile endTile = world.GetTile(target[0]);
        if (spectrum.GetElementPower(Elements.Earth) > 1)
        {
            endTile.GetActor().AlterHealth(-2);
            return true;
        }
        else
        {
            return false;
        }
    }
}
