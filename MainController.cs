using Godot;
using System;

public partial class MainController : Node
{

    private World world;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
