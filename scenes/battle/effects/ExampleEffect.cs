using Godot;
using System;
using System.Collections.Generic;

public partial class ExampleEffect : Effect
{
    public ExampleEffect(Actor actor) : base("example", "Example", "res://assets/bleed.jpg", 3, actor) { }

    public override bool AbilityExecuted(Actor source, World world, List<Vector2> target, ElementalSpectra spectra)
    {
        return true;
    }
}
