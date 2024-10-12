using Godot;
using System;
using System.Collections.Generic;

public partial class HurtSelfAbility : Ability
{
	public HurtSelfAbility(string inputAction) : base(inputAction) 
	{
		Key = RegisteredAbilities.HurtSelf;
		DisplayName = "Hurt Self";
		IconAsset = "res://assets/hurt_self.jpg";

		BaseUsesPerTurn = 3;
		RemainingUsesPerTurn = 3;

		BaseCooldown = 1;
		RemainingCooldown = 0;

		BaseNumberOfTargets = 0;
		BaseActionPointCost = 1;
	}

	public override void Execute(AbilityExecution execution)
	{
		execution.BaseDamage = 1;
	}
}
