using Godot;
using System;
using System.Collections.Generic;

public partial class BleedEffect : Effect
{
    public BleedEffect(Actor actor) : base("bleed", "Bleed", "res://assets/bleed.jpg", 3, actor) { }

    public override void EndTurn()
    {
        if (CurrentDuration > 0)
        {
            actor.AlterHealth(-1);
        }
        CurrentDuration = Mathf.Max(CurrentDuration - 1, 0);
    }

    public override bool AbilityExecuted(Actor source, World world, List<Vector2> target, ElementalSpectra spectra)
    {
        return true;
    }
}
