using Godot;
using System;
using System.Collections.Generic;

public partial class ExampleAbility : Ability
{
    public ExampleAbility(string inputAction) : base(inputAction, "example", "Example", 1, 0, 1) { }

    public override bool Execute(Actor source, World world, List<Vector2> target)
    {
        return true;
    }
}
