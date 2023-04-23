using Godot;
using System;

public partial class Main : Node
{
    private World world;
    private Vector2 highlightedCoords;

    private string pendingInputAction;

    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (Input.IsActionJustPressed("Q"))
        {
            pendingInputAction = "Q";
        }
    }

    public void HandleTileClick(Vector2 coords)
    {
        this.highlightedCoords = coords;
        world.HighlightCoordinates(coords);

        if (pendingInputAction != null)
        {
            world.ExecuteAction(pendingInputAction, coords);
            pendingInputAction = null;
        }
    }
}
