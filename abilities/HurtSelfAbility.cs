using Godot;
using System;
using System.Collections.Generic;

public partial class HurtSelfAbility : Ability
{
    public HurtSelfAbility(string inputAction) : base(inputAction, "hurtSelf", "Hurt Self", 1, 0, 0) { }

    public override bool Execute(Actor source, World world, List<Vector2> target)
    {
        source.AlterHealth(-1);
        return true;
    }
}
