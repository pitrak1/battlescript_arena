using Godot;
using System;
using System.Collections.Generic;

public partial class BleedEffect : ActorEffect
{
    public BleedEffect(Actor actor) : base("bleed", "Bleed", "res://assets/bleed.jpg", 3, actor) { }

    public override bool OnActorTurnEnd(
        World world,
        TurnOrder turnOrder,
        ElementalSpectra elementalSpectra
    ) {
        actor.AlterHealth(-1);
        return DecreaseAndRemoveIfNecessary(world, turnOrder, elementalSpectra);
    }
}
