using Godot;
using System.Collections.Generic;

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
    private PackedScene actorScene = GD.Load<PackedScene>("res://actors/Actor.tscn");
    private PackedScene tileScene = GD.Load<PackedScene>("res://tiles/Tile.tscn");

    public Actor HandleTileClick(Vector2 coords)
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                tiles[x, y].Highlight(coords == new Vector2(x, y));
            }
        }
        return tiles[(int)coords.X, (int)coords.Y].GetActor();
    }

    public void Setup()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                TileType type = testLevel[x, y];
                TileConfig tileConfig = new TileConfig(type);
                Tile tile = tileScene.Instantiate<Tile>();
                tile.Setup(new Vector2(x, y), tileConfig.Texture);
                AddChild(tile);
                tiles[x, y] = tile;
            }
        }

        Actor wolfActor = actorScene.Instantiate<Actor>();
        wolfActor.SetType(ActorType.Wolf);
        tiles[3, 3].PlaceActor(wolfActor);
        currentActor = wolfActor;
        currentActor.AddAbility(new MoveAbility("Q"));
        currentActor.AddAbility(new HurtSelfAbility("W"));
        currentActor.SetMaxHealth(15);
    }

    public Tile GetTile(Vector2 coordinates)
    {
        return tiles[(int)coordinates.X, (int)coordinates.Y];
    }

    public void ExecuteAbility(string inputAction, List<Vector2> target)
    {
        currentActor.ExecuteAbility(inputAction, currentActor, this, target);
    }
}
