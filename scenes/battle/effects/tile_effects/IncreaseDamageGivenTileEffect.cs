using Godot;
using System;
using System.Collections.Generic;

public partial class IncreaseDamageGivenTileEffect : TileEffect
{
    public IncreaseDamageGivenTileEffect(Tile tile) : base(tile)
    { 
        Key = RegisteredTileEffects.IncreaseDamageGiven;
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
}
