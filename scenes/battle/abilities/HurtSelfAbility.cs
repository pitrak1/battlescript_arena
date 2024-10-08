using Godot;
using System;
using System.Collections.Generic;

public partial class HurtSelfAbility : Ability
{
    public HurtSelfAbility(string inputAction) : base(inputAction, "hurtSelf", "Hurt Self", "res://assets/hurt_self.jpg", 3, 1, 0) { }

    public override bool ExecuteAction(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra)
    {
        source.AlterHealth(-1);
        return true;
    }
}
