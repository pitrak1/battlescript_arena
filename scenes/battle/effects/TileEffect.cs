using Godot;
using System;
using System.Collections.Generic;

public enum RegisteredTileEffects {
    IncreaseDamageGiven,
    IncreaseDamageTaken
}

public abstract partial class TileEffect : Effect
{
    public RegisteredTileEffects Key;
    public Tile CurrentTile;

    public TileEffect(Tile t)
    {
        CurrentTile = t;
    }

    public virtual bool OnAbilityExecutedAsSource(
        World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) {
        return false;
    }

    public virtual bool OnAbilityExecutedAsTarget(World world, 
        TurnOrder turnOrder, 
        ElementalSpectra elementalSpectra
    ) {
        return false;
    }
}
