using Godot;
using System;

public partial class Main : Node
{
    private World world;

    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();
    }
}
