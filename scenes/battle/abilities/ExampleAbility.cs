using Godot;
using System;
using System.Collections.Generic;

public partial class ExampleAbility : Ability
{
    public ExampleAbility(string inputAction) : base(inputAction, "example", "Example", 1, 1, 1) { }

    public override bool ExecuteAction(Actor source, World world, List<Vector2> target, ElementalSpectra spectra)
    {
        return true;
    }
}
