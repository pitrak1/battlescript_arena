using Godot;
using System;
using System.Collections.Generic;

public partial class ExampleAbility : Ability
{
    public ExampleAbility(string key, string displayName, int usesPerTurn, int cooldown, string inputAction) : base(key, displayName, usesPerTurn, cooldown, inputAction) { }

    public override bool Execute(Actor source, World world, List<Vector2> target)
    {
        return true;
    }
}
