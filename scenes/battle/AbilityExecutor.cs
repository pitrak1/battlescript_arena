using Godot;
using System;
using System.Collections.Generic;

public partial class AbilityExecutor : Node
{
    private World world;
    private TurnOrder turnOrder;
    private ElementalSpectra elementalSpectra;

    public AbilityExecutor(World worldState, TurnOrder turnOrderState, ElementalSpectra spectraState) 
    {
        world = worldState;
        turnOrder = turnOrderState;
        elementalSpectra = spectraState;
    }

    public void Execute(Ability ability, Actor source, List<Vector2> targets)
    {
		AbilityExecution baseAbilityExecution = new AbilityExecution(new Vector2(-1, -1));
        List<AbilityExecution> abilityExecutionsByTarget = new List<AbilityExecution>();

        // Add the initial data for the ability
        ability.Execute(baseAbilityExecution);

        GD.Print("BaseAbilityDamage: " + baseAbilityExecution.BaseDamage);
        GD.Print("BaseDamageMultiplier: " + baseAbilityExecution.DamageMultiplier);

        // Get starting tile, iterate through effects, and remove when necessary
        Tile sourceTile = world.GetTileAtCoordinates(source.Coordinates);
        applySourceTileEffects(sourceTile, baseAbilityExecution);

        // Iterate through actor effects and remove when necessary
        applySourceActorEffects(source, baseAbilityExecution);

        GD.Print("BaseAbilityDamage: " + baseAbilityExecution.BaseDamage);
        GD.Print("BaseDamageMultiplier: " + baseAbilityExecution.DamageMultiplier);

        // Create AbilityExecutions for each target
        foreach (Vector2 target in targets) {
            abilityExecutionsByTarget.Add(new AbilityExecution(target, baseAbilityExecution));
        }

        // Iterate through the AbilityExecutions, applying the target tile and actor effects and executing
        foreach (AbilityExecution targetExecution in abilityExecutionsByTarget) {
            GD.Print("TargetAbilityDamage: " + targetExecution.BaseDamage);
            GD.Print("TargetDamageMultiplier: " + targetExecution.DamageMultiplier);

            Tile targetTile = world.GetTileAtCoordinates(targetExecution.Coordinates);
            applyTargetTileEffects(targetTile, targetExecution);
            applyTargetActorEffects(targetTile.CurrentActor, targetExecution);
            targetExecution.Execute(world, turnOrder, elementalSpectra);

            GD.Print("TargetAbilityDamage: " + targetExecution.BaseDamage);
            GD.Print("TargetDamageMultiplier: " + targetExecution.DamageMultiplier);
        }
    }

    private void applySourceTileEffects(Tile tile, AbilityExecution execution) 
    {
        List<TileEffect> effectsToBeRemoved = new List<TileEffect>();

        foreach(TileEffect effect in tile.Effects) {
            bool remove = effect.OnAbilityExecutedAsSource(execution, world, turnOrder, elementalSpectra);
            if (remove) { effectsToBeRemoved.Add(effect); }
        }

        foreach(TileEffect effectToBeRemoved in effectsToBeRemoved) {
            tile.Effects.Remove(effectToBeRemoved);
        }
    }

    private void applyTargetTileEffects(Tile tile, AbilityExecution execution) 
    {
        List<TileEffect> effectsToBeRemoved = new List<TileEffect>();

        foreach(TileEffect effect in tile.Effects) {
            bool remove = effect.OnAbilityExecutedAsTarget(execution, world, turnOrder, elementalSpectra);
            if (remove) { effectsToBeRemoved.Add(effect); }
        }

        foreach(TileEffect effectToBeRemoved in effectsToBeRemoved) {
            tile.Effects.Remove(effectToBeRemoved);
        }
    }

    private void applySourceActorEffects(Actor actor, AbilityExecution execution)
    {
        List<ActorEffect> effectsToBeRemoved = new List<ActorEffect>();

        foreach(ActorEffect effect in actor.Effects) {
            bool remove = effect.OnAbilityExecutedAsSource(execution, world, turnOrder, elementalSpectra);
            if (remove) { effectsToBeRemoved.Add(effect); }
        }

        foreach(ActorEffect effectToBeRemoved in effectsToBeRemoved) {
            actor.Effects.Remove(effectToBeRemoved);
        }
    }

    private void applyTargetActorEffects(Actor actor, AbilityExecution execution)
    {
        List<ActorEffect> effectsToBeRemoved = new List<ActorEffect>();

        foreach(ActorEffect effect in actor.Effects) {
            bool remove = effect.OnAbilityExecutedAsTarget(execution, world, turnOrder, elementalSpectra);
            if (remove) { effectsToBeRemoved.Add(effect); }
        }

        foreach(ActorEffect effectToBeRemoved in effectsToBeRemoved) {
            actor.Effects.Remove(effectToBeRemoved);
        }
    }
}