using Godot;
using System.Collections.Generic;

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

public partial class World : Node
{
    private TileType[,] testLevel = new TileType[9, 9] {
        { TileType.None,     TileType.None,   TileType.High,  TileType.Middle,    TileType.Low,   TileType.Low,   TileType.None,   TileType.None,   TileType.None},
        { TileType.None,     TileType.Rock,  TileType.High,  TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.None,   TileType.None},
        { TileType.High,    TileType.High,  TileType.Rock,  TileType.Low,   TileType.Water,     TileType.Tree,  TileType.Low,   TileType.Low,   TileType.None},
        { TileType.Middle,  TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.Water,     TileType.Tree,  TileType.Low,   TileType.Low},
        { TileType.Low,     TileType.Low,   TileType.Water,     TileType.Low,   TileType.Tree,  TileType.Low,   TileType.Water,     TileType.Low,   TileType.Low},
        { TileType.Low,     TileType.Low,   TileType.Tree,  TileType.Water,     TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.Middle},
        { TileType.None,     TileType.Low,   TileType.Low,   TileType.Tree,  TileType.Water,     TileType.Low,   TileType.Rock,  TileType.High,  TileType.High},
        { TileType.None,     TileType.None,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.Low,   TileType.High,  TileType.Rock,  TileType.None},
        { TileType.None,     TileType.None,   TileType.None,   TileType.Low,   TileType.Low,   TileType.Middle,    TileType.High,  TileType.None,   TileType.None}
    };

    private Tile[,] tiles = new Tile[9, 9];

    private PackedScene tileScene = GD.Load<PackedScene>("res://Tile.tscn");

    private Vector2 highlightedCoords;

    public void HandleClick(Vector2 coords)
    {
        this.highlightedCoords = coords;

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                var child = tiles[x, y];
                child.Highlight(coords == new Vector2(x, y));
            }
        }
    }

    public void Setup()
    {
        Texture2D lowTexture = GD.Load<Texture2D>("res://assets/low.png");
        Vector2 offset = new Vector2(400, 200);

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                TileType type = testLevel[x, y];
                Tile tile = tileScene.Instantiate<Tile>();
                tile.Setup(type, new Vector2(x, y));
                AddChild(tile);
                tiles[x, y] = tile;
            }
        }
    }
}
