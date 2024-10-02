using Godot;
using System;
using System.Collections.Generic;

public abstract partial class ActorEffect : Effect
{
    public Actor actor;

    public ActorEffect(
        string key, 
        string displayName, 
        string iconAsset, 
        int duration, 
        Actor a
    ) : base(
        key, 
        displayName, 
        iconAsset, 
        duration)
    {
        actor = a;
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
