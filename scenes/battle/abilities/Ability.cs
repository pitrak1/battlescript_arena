using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Ability : Node
{
    public string Key;
    public string DisplayName;
    public string InputAction;
    public string IconAsset;

    public int BaseUsesPerTurn;
    public int RemainingUsesPerTurn;

    public int BaseCooldown;
    public int RemainingCooldown;

    public int BaseNumberOfTargets;
    public int BaseActionPointCost;

    public Ability(
        string key, 
        string displayName,
        string inputAction, 
        string iconAsset, 
        int usesPerTurn, 
        int cooldown, 
        int numberOfTargets,
        int actionPointCost
    )
    {
        Key = key;
        DisplayName = displayName;
        InputAction = inputAction;
        IconAsset = iconAsset;

        BaseUsesPerTurn = usesPerTurn;
        RemainingUsesPerTurn = usesPerTurn;

        BaseCooldown = cooldown;
        RemainingCooldown = 0;

        BaseNumberOfTargets = numberOfTargets;
        BaseActionPointCost = actionPointCost;
    }

    public abstract void Execute(AbilityExecution execution);
}
