using Godot;
using System;
using System.Collections.Generic;

public partial class IncreaseDamageGivenEffect : ActorEffect
{
    public IncreaseDamageGivenEffect(Actor actor) : base(actor)
    { 
        Key = RegisteredActorEffects.IncreaseDamageGiven;
        DisplayName = "Increase Damage Given";
        IconAsset = "res://assets/bleed.jpg";
        BaseDuration = 3;
        RemainingDuration = 3;
    }

    public override bool OnAbilityExecutedAsSource(AbilityExecution execution, World world, TurnOrder turnOrder, ElementalSpectra elementalSpectra)
    {
        execution.DamageMultiplier += 0.5;
        return false;
    }

    public override bool OnActorTurnEnd(
        World world,
        TurnOrder turnOrder,
        ElementalSpectra elementalSpectra
    ) {
        return DecreaseAndRemoveIfNecessary(world, turnOrder, elementalSpectra);
    }
}
