using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Ability : Node
{
    public string Key;
    public string DisplayName;
    public int UsesPerTurn;
    public int Cooldown;

    public int CurrentUsesPerTurn;
    public int CurrentCooldown;
    public string InputAction;

    public int NumberOfTargets;

    public Ability(string inputAction, string key, string displayName, int usesPerTurn, int cooldown, int numberOfTargets = 1)
    {
        InputAction = inputAction;
        Key = key;
        DisplayName = displayName;
        UsesPerTurn = usesPerTurn;
        Cooldown = cooldown;
        NumberOfTargets = numberOfTargets;
        Reset();
    }

    public void TurnReset()
    {
        CurrentUsesPerTurn = UsesPerTurn;
    }

    public void Reset()
    {
        CurrentUsesPerTurn = UsesPerTurn;
        CurrentCooldown = Cooldown;
    }


    public abstract bool Execute(Actor source, World world, List<Vector2> target);
}
