using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Effect : Node
{
    public string DisplayName;
    public string IconAsset;
    public int BaseDuration;
    public int RemainingDuration;

    public virtual bool OnTurnEnd(
        World world,
        TurnOrder turnOrder,
        ElementalSpectra elementalSpectra
    ) {
        return false;
    }

    public virtual bool OnTurnStart(
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) { 
        return false; 
    }

    public virtual bool OnAbilityExecutedAsSource(
        AbilityExecution execution,
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) {
        return false;
    }

    public virtual bool OnAbilityExecutedAsTarget(
        AbilityExecution execution,
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) {
        return false;
    }

    protected bool DecreaseAndRemoveIfNecessary(
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) {
        RemainingDuration = Mathf.Max(RemainingDuration - 1, 0);
        return RemainingDuration <= 0;
    }
}
