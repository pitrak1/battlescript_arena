using Godot;
using System;
using System.Collections.Generic;

public partial class AbilityExecution : Node
{
    public Ability AbilityReference;
    public int InProgressUsesPerTurn;
    public int InProgressCooldown;
    public int InProgressNumberOfTargets;
    public int InProgressActionPointCost;

    public Actor Source;
    public List<Vector2> Targets;
    public World WorldState;
    public TurnOrder TurnOrderState;
    public ElementalSpectra ElementalSpectraState;

    public int BaseDamage = 0;
    public double DamageMultiplier = 1;

    public AbilityExecution(
        Ability ability,
        Actor source,
        List<Vector2> targets,
        World world,
        TurnOrder turnOrder,
        ElementalSpectra elementalSpectra
    )
    {
        AbilityReference = ability;
        Source = source;
        Targets = targets;
        WorldState = world;
        TurnOrderState = turnOrder;
        ElementalSpectraState = elementalSpectra;
    }

    public void Execute() {
    }
}
