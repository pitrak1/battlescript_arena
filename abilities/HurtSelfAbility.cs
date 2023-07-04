using Godot;
using System;
using System.Collections.Generic;

public partial class HurtSelfAbility : Ability
{
    public HurtSelfAbility(string inputAction) : base(inputAction, "hurtSelf", "Hurt Self", 1, 1, 0) { }

    public override bool ExecuteAction(Actor source, World world, List<Vector2> target, Spectrum spectrum)
    {
        source.AlterHealth(-1);
        return true;
    }
}
