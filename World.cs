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
    private List<Actor> actors = new List<Actor>();
    private Actor currentActor;


    private Dictionary<TileType, PackedScene> tileScenes = new Dictionary<TileType, PackedScene> {
        {TileType.Low, GD.Load<PackedScene>("res://tiles/LowTile.tscn")},
        {TileType.Middle, GD.Load<PackedScene>("res://tiles/MiddleTile.tscn")},
        {TileType.High, GD.Load<PackedScene>("res://tiles/HighTile.tscn")},
        {TileType.Tree, GD.Load<PackedScene>("res://tiles/TreeTile.tscn")},
        {TileType.Rock, GD.Load<PackedScene>("res://tiles/RockTile.tscn")},
        {TileType.Water, GD.Load<PackedScene>("res://tiles/WaterTile.tscn")},
        {TileType.None, GD.Load<PackedScene>("res://tiles/EmptyTile.tscn")},
    };
    private PackedScene wolfActorScene = GD.Load<PackedScene>("res://actors/WolfActor.tscn");

    public void HighlightCoordinates(Vector2 coords)
    {
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
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                TileType type = testLevel[x, y];
                Tile tile = tileScenes[type].Instantiate<Tile>();
                tile.Setup(new Vector2(x, y));
                AddChild(tile);
                tiles[x, y] = tile;
            }
        }

        WolfActor actor = wolfActorScene.Instantiate<WolfActor>();
        actor.Setup();
        tiles[3, 3].PlaceActor(actor);
        currentActor = actor;
        currentActor.AddAction(new MoveAction("move", 1, 0, "Q"));
    }

    public Tile GetTile(Vector2 coordinates)
    {
        return tiles[(int)coordinates.X, (int)coordinates.Y];
    }

    public void ExecuteAction(string inputAction, Vector2 target)
    {
        currentActor.ExecuteAction(inputAction, currentActor, this, target);
    }
}
