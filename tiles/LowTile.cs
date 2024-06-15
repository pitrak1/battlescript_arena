using Godot;
using System;

public partial class LowTile : Tile
{
    public override void Setup(Vector2 coords)
    {
        base.Setup(coords);
        this.Texture = GD.Load<Texture2D>("res://assets/low.png");
    }
}
