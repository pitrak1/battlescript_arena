using Godot;
using System;

public partial class ExampleAction : Action
{
    public ExampleAction(string key, int usesPerTurn, int cooldown, string inputAction) : base(key, usesPerTurn, cooldown, inputAction) { }

    public override bool Execute(Actor source, World world, Vector2 target)
    {
        return true;
    }
}
