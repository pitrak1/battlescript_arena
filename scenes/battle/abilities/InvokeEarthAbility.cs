using Godot;
using System;
using System.Collections.Generic;

public partial class InvokeEarthAbility : Ability
{
    public InvokeEarthAbility(string inputAction) : base(inputAction, "invokeEarth", "Invoke Earth", 1, 1, 0) { }

    public override bool ExecuteAction(Actor source, World world, List<Vector2> target, Spectrum spectrum)
    {
        GD.Print(spectrum.GetElementPower(Elements.Earth));
        spectrum.IncreaseElement(Elements.Earth);
        return true;
    }
}
