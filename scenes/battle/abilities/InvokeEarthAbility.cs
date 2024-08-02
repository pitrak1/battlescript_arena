using Godot;
using System;
using System.Collections.Generic;

public partial class InvokeEarthAbility : Ability
{
    public InvokeEarthAbility(string inputAction) : base(inputAction, "invokeEarth", "Invoke Earth", 1, 1, 0) { }

    public override bool ExecuteAction(Actor source, List<Vector2> targets, World world, TurnOrder turnOrder, ElementalSpectra spectra)
    {
        GD.Print(spectra.GetElementPower(Elements.Earth));
        spectra.IncreaseElement(Elements.Earth);
        return true;
    }
}
