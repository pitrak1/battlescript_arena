using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Ability : Node
{
    public string Key;
    public string DisplayName;
    public int UsesPerTurn;
    public int Cooldown;
    public int NumberOfTargets;
    public string InputAction;

    public int CurrentUsesPerTurn;
    public int CurrentCooldown;

    public string IconAsset;


    public Ability(string inputAction, string key, string displayName, string iconAsset, int usesPerTurn, int cooldown, int numberOfTargets = 1)
    {
        Key = key;
        DisplayName = displayName;
        IconAsset = iconAsset;
        UsesPerTurn = usesPerTurn;
        Cooldown = cooldown;
        NumberOfTargets = numberOfTargets;
        InputAction = inputAction;

        CurrentUsesPerTurn = UsesPerTurn;
        CurrentCooldown = 0;
    }

    public void EndTurn()
    {
        CurrentUsesPerTurn = UsesPerTurn;
        CurrentCooldown = Mathf.Max(CurrentCooldown - 1, 0);
    }

    public void Reset()
    {
        CurrentUsesPerTurn = UsesPerTurn;
        CurrentCooldown = Cooldown;
    }


    public bool Execute(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra)
    {
        if (CurrentUsesPerTurn == 0 || CurrentCooldown > 0) return false;

        bool result = this.ExecuteAction(source, targets, world, turnOrder, spectra);

        if (result)
        {
            CurrentUsesPerTurn--;
        }

        if (CurrentUsesPerTurn == 0)
        {
            CurrentCooldown = Cooldown;
        }

        return result;
    }

    public abstract bool ExecuteAction(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra);
}
