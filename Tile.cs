using Godot;
using System.Collections.Generic;

public partial class Tile : Sprite2D
{
    private SortedDictionary<TileType, Texture2D> textureMap = new SortedDictionary<TileType, Texture2D> {
        {TileType.Low, GD.Load<Texture2D>("res://assets/low.png")},
        {TileType.Middle, GD.Load<Texture2D>("res://assets/middle.png")},
        {TileType.High, GD.Load<Texture2D>("res://assets/high.png")},
        {TileType.Rock, GD.Load<Texture2D>("res://assets/rock.png")},
        {TileType.Tree, GD.Load<Texture2D>("res://assets/tree.png")},
        {TileType.Water, GD.Load<Texture2D>("res://assets/water.png")}
    };

    public void SetType(TileType type)
    {
        this.Texture = textureMap[type];
    }

    public void _on_static_body_2d_input_event(Node viewport, InputEvent inputEvent, int shape_idx)
    {
        if (Input.IsActionJustPressed("LMB"))
        {
            GD.Print("GOTHERE");
        }

    }
}
