using Godot;
using System;

public abstract partial class Ability : Node
{
    public string Key;
    public string DisplayName;
    public int UsesPerTurn;
    public int Cooldown;

    public int CurrentUsesPerTurn;
    public int CurrentCooldown;

    public string InputAction;

    public Ability(string key, string displayName, int usesPerTurn, int cooldown, string inputAction)
    {
        Key = key;
        DisplayName = displayName;
        UsesPerTurn = usesPerTurn;
        Cooldown = cooldown;
        InputAction = inputAction;
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


    public abstract bool Execute(Actor source, World world, Vector2 target);
}
