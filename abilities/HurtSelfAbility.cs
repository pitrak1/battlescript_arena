using Godot;
using System;
using System.Collections.Generic;

public partial class HurtSelfAbility : Ability
{
    public HurtSelfAbility(string key, string displayName, int usesPerTurn, int cooldown, string inputAction) : base(key, displayName, usesPerTurn, cooldown, inputAction, 0) { }

    public override bool Execute(Actor source, World world, List<Vector2> target)
    {
        source.AlterHealth(-1);
        return true;
    }
}
