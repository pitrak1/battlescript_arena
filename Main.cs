using Godot;
using System;

public partial class Main : Node
{
    private World world;
    private Vector2 highlightedCoords;

    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();
    }

    public void HandleTileClick(Vector2 coords)
    {
        this.highlightedCoords = coords;
        world.HighlightCoordinates(coords);
    }
}
