using Godot;
using System;
using System.Collections.Generic;

public enum RegisteredAbilities {
    HurtSelf,
    InvokeEarth,
    Move,
    ThrowDirt
}

public abstract partial class Ability : Node
{
    public RegisteredAbilities Key;
    public string DisplayName;
    public string IconAsset;

    public int BaseUsesPerTurn;
    public int RemainingUsesPerTurn;

    public int BaseCooldown;
    public int RemainingCooldown = 0;

    public int BaseNumberOfTargets;
    public int BaseActionPointCost;

    public string InputAction;

    public Ability(string inputAction) {
        InputAction = inputAction;
    }

    public abstract void Execute(AbilityExecution execution);
}
