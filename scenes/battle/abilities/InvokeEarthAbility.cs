using Godot;
using System;
using System.Collections.Generic;

public partial class InvokeEarthAbility : Ability
{
    public InvokeEarthAbility(string inputAction) : base(inputAction) 
	{
		Key = RegisteredAbilities.InvokeEarth;
		DisplayName = "Invoke Earth";
		IconAsset = "res://assets/invoke_earth.jpg";

		BaseUsesPerTurn = 1;
		RemainingUsesPerTurn = 1;

		BaseCooldown = 1;
		RemainingCooldown = 0;

		BaseNumberOfTargets = 0;
		BaseActionPointCost = 1;
	}

    public override void Execute(AbilityExecution execution)
    {
        // execution.ElementalSpectraState.EarthPower = 3;
    }
}
