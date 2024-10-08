using Godot;
using System;
using System.Collections.Generic;

public partial class InvokeEarthAbility : Ability
{
    public InvokeEarthAbility(string inputAction) : base(inputAction, "invokeEarth", "Invoke Earth", "res://assets/invoke_earth.jpg", 1, 1, 0) { }

    public override bool ExecuteAction(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra)
    {
        spectra.EarthPower++;
        return true;
    }
}
