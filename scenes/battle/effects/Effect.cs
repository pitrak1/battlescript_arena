using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Effect : Node
{
    public string Key;
    public string DisplayName;
    public int BaseDuration;
    public int RemainingDuration;

    public Effect(string key, string displayName, string iconAsset, int duration)
    {
        Key = key;
        DisplayName = displayName;
        BaseDuration = duration;
        RemainingDuration = BaseDuration;
    }

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

    protected bool DecreaseAndRemoveIfNecessary(
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) {
        RemainingDuration = Mathf.Max(RemainingDuration - 1, 0);
        return RemainingDuration <= 0;
    }
}
