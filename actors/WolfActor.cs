using Godot;
using System;

public partial class WolfActor : Actor
{
    public override void _Ready()
    {
        this.Texture = GD.Load<Texture2D>("res://assets/wolf.jpeg");
        this.Speed = 15;
        this.DisplayName = "Wolf";

        AddAbility(new MoveAbility("Q"));
        AddAbility(new HurtSelfAbility("W"));
        AddAbility(new InvokeEarthAbility("E"));
        SetMaxHealth(15);
    }
}
