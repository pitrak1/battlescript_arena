using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Effect : Node
{
    public string Key;
    public string DisplayName;
    public int Duration;
    public int CurrentDuration;
    public Actor actor;


    public Effect(string key, string displayName, int duration, Actor a)
    {
        Key = key;
        DisplayName = displayName;
        Duration = duration;
        actor = a;

        CurrentDuration = Duration;
    }

    public virtual void EndTurn()
    {
        CurrentDuration = Mathf.Max(CurrentDuration - 1, 0);
    }

    public void Reset()
    {
        CurrentDuration = Duration;
    }

    public virtual bool AbilityExecuted(Actor source, World world, List<Vector2> target, ElementalSpectra spectra)
    {
        return true;
    }
}
