using Godot;
using System.Collections.Generic;

enum TileType
{
    High,
    Middle,
    Low,
    Water,
    Rock,
    Tree
}

public partial class World : Node
{
    private TileType?[,] testLevel = new TileType?[9, 9] {
        { null,     null,   TileType.High,  TileType.Middle,    TileType.Low,   TileType.Low,   null,   null,   null},
        { null,     TileType.Rock,  TileType.High,  TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   null,   null},
        { TileType.High,    TileType.High,  TileType.Rock,  TileType.Low,   TileType.Water,     TileType.Tree,  TileType.Low,   TileType.Low,   null},
        { TileType.Middle,  TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.Water,     TileType.Tree,  TileType.Low,   TileType.Low},
        { TileType.Low,     TileType.Low,   TileType.Water,     TileType.Low,   TileType.Tree,  TileType.Low,   TileType.Water,     TileType.Low,   TileType.Low},
        { TileType.Low,     TileType.Low,   TileType.Tree,  TileType.Water,     TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.Middle},
        { null,     TileType.Low,   TileType.Low,   TileType.Tree,  TileType.Water,     TileType.Low,   TileType.Rock,  TileType.High,  TileType.High},
        { null,     null,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.High,  TileType.Rock,  null},
        { null,     null,   null,   TileType.Low,   TileType.Low,   TileType.Middle,    TileType.High,  null,   null}
    };

    private SortedDictionary<TileType, Texture2D> textureMap = new SortedDictionary<TileType, Texture2D> {
        {TileType.Low, GD.Load<Texture2D>("res://assets/low.png")},
        {TileType.Middle, GD.Load<Texture2D>("res://assets/middle.png")},
        {TileType.High, GD.Load<Texture2D>("res://assets/high.png")},
        {TileType.Rock, GD.Load<Texture2D>("res://assets/rock.png")},
        {TileType.Tree, GD.Load<Texture2D>("res://assets/tree.png")},
        {TileType.Water, GD.Load<Texture2D>("res://assets/water.png")}
    };

    public void Setup()
    {
        Texture2D lowTexture = GD.Load<Texture2D>("res://assets/low.png");
        Vector2 offset = new Vector2(400, 200);

        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 9; i++)
            {
                TileType? type = testLevel[i, j];
                if (type is null) continue;

                Sprite2D tile = new Sprite2D();
                tile.Texture = textureMap[(TileType)type];
                tile.Position = new Vector2((i - (j * 0.5f)) * 64, j * 43) + offset;
                AddChild(tile);
            }
        }
    }
}
