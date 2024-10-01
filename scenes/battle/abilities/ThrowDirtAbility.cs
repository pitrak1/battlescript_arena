using Godot;
using System;
using System.Collections.Generic;

public partial class ThrowDirtAbility : Ability
{
    public ThrowDirtAbility(string inputAction) : base(
        inputAction, 
        "throwDirt", 
        "Throw Dirt", 
        "res://assets/throw_dirt.jpg", 
        2, 
        0,
        1,
        3
    ) { }

    public override void Execute(AbilityExecution execution)
    {
    }
}
