using Godot;
using System.Collections.Generic;

public enum ActorType
{
    Wolf
}

public partial class Actor : Node
{
    private SortedDictionary<ActorType, Texture2D> textureMap = new SortedDictionary<ActorType, Texture2D> {
        {ActorType.Wolf, GD.Load<Texture2D>("res://assets/wolf.png")},
    };

    public Vector2 coordinates;

    public void Setup(TileType type, Vector2 coords)
    {
    }
}
