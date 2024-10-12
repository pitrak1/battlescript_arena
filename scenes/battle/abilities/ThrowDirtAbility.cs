using Godot;
using System;
using System.Collections.Generic;

public partial class ThrowDirtAbility : Ability
{
    public ThrowDirtAbility(string inputAction) : base(inputAction) 
	{
		Key = RegisteredAbilities.ThrowDirt;
		DisplayName = "Throw Dirt";
		IconAsset = "res://assets/throw_dirt.jpg";

		BaseUsesPerTurn = 2;
		RemainingUsesPerTurn = 2;

		BaseCooldown = 1;
		RemainingCooldown = 0;

		BaseNumberOfTargets = 1;
		BaseActionPointCost = 3;
	}

    public override void Execute(AbilityExecution execution)
    {
        execution.BaseDamage = 5;
    }
}
