using Godot;
using System;

public partial class EmptyTile : Tile
{
    public override void Setup(Vector2 coords)
    {
        base.Setup(coords);
        this.Visible = false;
    }
}
