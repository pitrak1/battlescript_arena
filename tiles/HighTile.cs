using Godot;
using System;

public partial class HighTile : Tile
{
    public override void Setup(Vector2 coords)
    {
        base.Setup(coords);
        this.Texture = GD.Load<Texture2D>("res://assets/high.png");
    }
}
