using Godot;
using System;
using System.Collections.Generic;

public partial class IncreaseDamageTakenTileEffect : TileEffect
{
    public IncreaseDamageTakenTileEffect(Tile tile) : base(tile)
    { 
        Key = RegisteredTileEffects.IncreaseDamageTaken;
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
}
