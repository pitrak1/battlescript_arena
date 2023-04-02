using Godot;
using System.Collections.Generic;

public enum ActorType
{
    Wolf
}

public partial class Actor : Sprite2D
{
    private SortedDictionary<ActorType, Texture2D> textureMap = new SortedDictionary<ActorType, Texture2D> {
        {ActorType.Wolf, GD.Load<Texture2D>("res://assets/wolf.jpeg")},
    };

    private Vector2 coordinates;

    public void Setup(ActorType type)
    {
        this.Texture = textureMap[type];
    }

    public void Place(Vector2 coords)
    {
        this.coordinates = coords;
    }
}
