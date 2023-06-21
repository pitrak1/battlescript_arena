using Godot;
using System;

public partial class NoneTile : Tile
{
    public override void Setup(Vector2 coords)
    {
        base.Setup(coords);
        this.Visible = false;
    }
}
