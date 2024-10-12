using Godot;
using System;
using System.Collections.Generic;

public partial class AbilityExecution : Node
{
    public Vector2 Coordinates;
    public int BaseDamage = 0;
    public double DamageMultiplier = 1;

    public AbilityExecution(
        Vector2 coordinates,
        int damage = 0,
        double multiplier = 1
    )
    {
        Coordinates = coordinates;
        BaseDamage = damage;
        DamageMultiplier = multiplier;
    }

    public AbilityExecution(Vector2 coordinates, AbilityExecution execution)
    {
        Coordinates = coordinates;
        BaseDamage = execution.BaseDamage;
        DamageMultiplier = execution.DamageMultiplier;
    }

    public void Execute(World world, TurnOrder turnOrder, ElementalSpectra elementalSpectra) {
        Actor targetActor = world.GetActorAtCoordinates(Coordinates);
        targetActor.AlterHealth((int)-Mathf.Floor(BaseDamage * DamageMultiplier));
    }
}
