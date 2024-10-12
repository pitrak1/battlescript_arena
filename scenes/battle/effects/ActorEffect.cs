using Godot;
using System;
using System.Collections.Generic;

public enum RegisteredActorEffects {
    Bleed,
    IncreaseDamageGiven,
    IncreaseDamageTaken
}

public abstract partial class ActorEffect : Effect
{
    public RegisteredActorEffects Key;
    public Actor CurrentActor;

    public ActorEffect(Actor a)
    {
        CurrentActor = a;
    }

    public virtual bool OnActorTurnStart(
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) { 
        return false; 
    }

    public virtual bool OnActorTurnEnd(
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) { 
        return false; 
    }
}
