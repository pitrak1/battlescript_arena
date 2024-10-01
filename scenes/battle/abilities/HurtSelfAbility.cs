using Godot;
using System;
using System.Collections.Generic;

public partial class HurtSelfAbility : Ability
{
    public HurtSelfAbility(string inputAction) : base(
        "hurtSelf", 
        "Hurt Self",
        inputAction,
        "res://assets/hurt_self.jpg", 
        3, 
        1,
        0,
        1
    ) { }

    public override void Execute(AbilityExecution execution)
    {
        execution.BaseDamage = 1;
    }
}
