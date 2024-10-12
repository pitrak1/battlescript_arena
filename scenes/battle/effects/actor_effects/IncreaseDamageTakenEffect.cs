using Godot;
using System;
using System.Collections.Generic;

public partial class IncreaseDamageTakenEffect : ActorEffect
{
    public IncreaseDamageTakenEffect(Actor actor) : base(actor)
    { 
        Key = RegisteredActorEffects.IncreaseDamageTaken;
        DisplayName = "Increase Damage Taken";
        IconAsset = "res://assets/bleed.jpg";
        BaseDuration = 3;
        RemainingDuration = 3;
    }

    public override bool OnAbilityExecutedAsTarget(AbilityExecution execution, World world, TurnOrder turnOrder, ElementalSpectra elementalSpectra)
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
