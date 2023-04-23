using Godot;
using System.Collections.Generic;

public enum ActorType
{
    Wolf
}


public partial class WolfActor : Actor
{
    public override void Setup()
    {
        base.Setup();
        this.Texture = GD.Load<Texture2D>("res://assets/wolf.jpeg");
    }
}
