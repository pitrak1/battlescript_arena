using Godot;
using System;
using System.Collections.Generic;

public partial class ExampleAbility : Ability
{
    public ExampleAbility(string inputAction) : base(inputAction, "example", "Example", "res://assets/move.jpg", 1, 1, 1) { }

    public override bool ExecuteAction(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra)
    {
        return true;
    }
}
