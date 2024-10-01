using Godot;
using System;
using System.Collections.Generic;

public partial class InvokeEarthAbility : Ability
{
    public InvokeEarthAbility(string inputAction) : base(
        inputAction, 
        "invokeEarth", 
        "Invoke Earth", 
        "res://assets/invoke_earth.jpg", 
        1, 
        1, 
        0,
        1
    ) { }

    public override void Execute(AbilityExecution execution)
    {
        execution.ElementalSpectraState.EarthPower = 3;
    }
}
