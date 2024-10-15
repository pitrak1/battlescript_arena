using Godot;
using System;

public partial class Sprite2d : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("READY");
		var staticBody = GetNode<StaticBody2D>("StaticBody2D");
		staticBody.MouseEntered += onMouseEntered;
		staticBody.InputPickable = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_input_event(Node viewport, InputEvent inputEvent, int shape_idx)
    {
        GD.Print("CLICKED");
        if (Input.IsActionJustPressed("LMB"))
        {
            // GetTree().CallGroup("InputReceivers", "_onTileClicked", coordinates);
        }
    }

	private void onMouseEntered()
	{
		GD.Print("ENTERED");
	}
}
