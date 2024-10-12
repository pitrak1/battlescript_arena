using Godot;
using System;
using System.Collections.Generic;

public partial class BleedEffect : ActorEffect
{
    public BleedEffect(Actor actor) : base(actor)
    { 
        Key = RegisteredActorEffects.Bleed;
        DisplayName = "Bleed";
        IconAsset = "res://assets/bleed.jpg";
        BaseDuration = 3;
        RemainingDuration = 3;
    }

    public override bool OnActorTurnEnd(
        World world,
        TurnOrder turnOrder,
        ElementalSpectra elementalSpectra
    ) {
        CurrentActor.AlterHealth(-1);
        return DecreaseAndRemoveIfNecessary(world, turnOrder, elementalSpectra);
    }
}
