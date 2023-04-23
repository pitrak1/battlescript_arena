using Godot;
using System;

public partial class Action : Node
{
    public string Key;
    public int UsesPerTurn;
    public int Cooldown;

    public int CurrentUsesPerTurn;
    public int CurrentCooldown;

    public string InputAction;

    public Action(string key, int usesPerTurn, int cooldown, string inputAction)
    {
        Key = key;
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


    public virtual bool Execute(Actor source, World world, Vector2 target)
    {
        return true;
    }
}
