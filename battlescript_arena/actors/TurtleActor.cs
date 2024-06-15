using Godot;
using System;

public partial class TurtleActor : Actor
{
    public override void _Ready()
    {
        this.Texture = GD.Load<Texture2D>("res://assets/turtle.jpeg");
        this.Speed = 20;
        this.DisplayName = "Turtle";

        AddAbility(new MoveAbility("Q"));
        AddAbility(new HurtSelfAbility("W"));
        AddAbility(new ThrowDirtAbility("E"));
        SetMaxHealth(20);
    }
}
