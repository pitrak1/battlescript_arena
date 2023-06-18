using Godot;
using System.Collections.Generic;

// YOUR LIST OF TILE TYPES HERE
public enum TileType
{
    High,
    Middle,
    Low,
    Water,
    Rock,
    Tree,
    None
}

public class TileConfig
{
    public TileType Type;
    public Texture2D Texture;

    public static Dictionary<TileType, Texture2D> TileTextureMap = new Dictionary<TileType, Texture2D>() {
        // YOUR MAPPINGS FROM TILETYPE TO YOUR PRIMARY TEXTURE HERE
        // EX: {TileType.High, GD.Load<Texture2D>("res://assets/high.png")}

        {TileType.High, GD.Load<Texture2D>("res://assets/high.png")},
        {TileType.Low, GD.Load<Texture2D>("res://assets/low.png")},
        {TileType.Middle, GD.Load<Texture2D>("res://assets/middle.png")},
        {TileType.Rock, GD.Load<Texture2D>("res://assets/rock.png")},
        {TileType.Water, GD.Load<Texture2D>("res://assets/water.png")},
        {TileType.Tree, GD.Load<Texture2D>("res://assets/tree.png")},
        {TileType.None, null},
    };

    public TileConfig(TileType type)
    {
        Type = type;
        Texture = TileConfig.TileTextureMap[type];
    }
}
