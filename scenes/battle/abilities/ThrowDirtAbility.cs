using Godot;
using System;
using System.Collections.Generic;

public partial class ThrowDirtAbility : Ability
{
    public ThrowDirtAbility(string inputAction) : base(inputAction, "throwDirt", "Throw Dirt", 2, 0, 1) { }

    public override bool ExecuteAction(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra)
    {
        // Tile startTile = world.GetTile(source.Coordinates);
        // Tile endTile = world.GetTile(target[0]);
        // if (ElementalSpectra.GetElementPower(Elements.Earth) > 1)
        // {
        //     Actor endActor = endTile.GetActor();
        //     endActor.AlterHealth(-2);
        //     endActor.Effects.Add(new BleedEffect(endActor));
        //     return true;
        // }
        // else
        // {
        //     return false;
        // }

        return true;
    }
}
