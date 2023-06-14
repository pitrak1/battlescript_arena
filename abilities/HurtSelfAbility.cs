using Godot;
using System;

public partial class HurtSelfAbility : Ability
{
    public HurtSelfAbility(string key, string displayName, int usesPerTurn, int cooldown, string inputAction) : base(key, displayName, usesPerTurn, cooldown, inputAction) { }

    public override bool Execute(Actor source, World world, Vector2 target)
    {
        source.AlterHealth(-1);
        return true;
    }
}
