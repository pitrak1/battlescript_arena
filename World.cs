using Godot;
using System.Collections.Generic;

public enum TileType
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

    private PackedScene tileScene = GD.Load<PackedScene>("res://Tile.tscn");

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

                Tile tile = tileScene.Instantiate<Tile>();
                tile.SetType((TileType)type);
                tile.Position = new Vector2((i - (j * 0.5f)) * 64, j * 43) + offset;
                AddChild(tile);
            }
        }
    }
}
