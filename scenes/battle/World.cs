using Godot;
using System.Collections.Generic;

public partial class World : Node2D
{
    private TileTypes[,] testLevel = new TileTypes[9, 9] {
        { TileTypes.None,     TileTypes.None,   TileTypes.High,  TileTypes.Middle,    TileTypes.Low,   TileTypes.Low,   TileTypes.None,   TileTypes.None,   TileTypes.None},
        { TileTypes.None,     TileTypes.Rock,  TileTypes.High,  TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.None,   TileTypes.None},
        { TileTypes.High,    TileTypes.High,  TileTypes.Rock,  TileTypes.Low,   TileTypes.Water,     TileTypes.Tree,  TileTypes.Low,   TileTypes.Low,   TileTypes.None},
        { TileTypes.Middle,  TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.Water,     TileTypes.Tree,  TileTypes.Low,   TileTypes.Low},
        { TileTypes.Low,     TileTypes.Low,   TileTypes.Water,     TileTypes.Low,   TileTypes.Tree,  TileTypes.Low,   TileTypes.Water,     TileTypes.Low,   TileTypes.Low},
        { TileTypes.Low,     TileTypes.Low,   TileTypes.Tree,  TileTypes.Water,     TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.Middle},
        { TileTypes.None,     TileTypes.Low,   TileTypes.Low,   TileTypes.Tree,  TileTypes.Water,     TileTypes.Low,   TileTypes.Rock,  TileTypes.High,  TileTypes.High},
        { TileTypes.None,     TileTypes.None,   TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.Low,   TileTypes.High,  TileTypes.Rock,  TileTypes.None},
        { TileTypes.None,     TileTypes.None,   TileTypes.None,   TileTypes.Low,   TileTypes.Low,   TileTypes.Middle,    TileTypes.High,  TileTypes.None,   TileTypes.None}
    };

    private Tile[,] tiles = new Tile[9, 9];

    public List<Actor> Actors { get; private set; } = new List<Actor>();

    private PackedScene tileScene;
    private PackedScene actorScene;

    public override void _Ready()
    {
        tileScene = GD.Load<PackedScene>("res://scenes/battle/Tile.tscn");
        actorScene = GD.Load<PackedScene>("res://scenes/battle/Actor.tscn");

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                TileTypes type = testLevel[x, y];
                if (type != TileTypes.None)
                {
                    Tile tile = tileScene.Instantiate<Tile>();
                    tile.Setup(new Vector2(x, y), type);
                    AddChild(tile);
                    tiles[x, y] = tile;
                }
            }
        }

        Actor wolf = AddActor(ActorTypes.Wolf, new Vector2(5, 5));
        Actor turtle = AddActor(ActorTypes.Turtle, new Vector2(7, 7));
        // RemoveActor(wolf);

    }

    public void HighlightTile(Vector2 coordinates)
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (tiles[x, y] is not null)
                {
                    tiles[x, y].Highlight(coordinates == new Vector2(x, y));
                }
            }
        }
    }

    public void ClearHighlightTile()
    {
        HighlightTile(new Vector2(10, 10));
    }

    public Actor AddActor(ActorTypes actorType, Vector2 coordinates)
    {
        Actor actor = actorScene.Instantiate<Actor>();
        actor.Setup(actorType, coordinates);
        GetTileAtCoordinates(coordinates).CurrentActor = actor;
        Actors.Add(actor);
        return actor;
    }

    public Actor RemoveActor(Actor actor)
    {
        GetTileAtCoordinates(actor.Coordinates).CurrentActor = null;
        Actors.Remove(actor);
        return actor;
    }

    public Tile GetTileAtCoordinates(Vector2 coordinates)
    {
        return tiles[(int)coordinates.X, (int)coordinates.Y];
    }

    public Actor GetActorAtCoordinates(Vector2 coordinates)
    {
        return GetTileAtCoordinates(coordinates).CurrentActor;
    }

    // public Tile GetTile(Vector2 coordinates)
    // {
    //     return tiles[(int)coordinates.X, (int)coordinates.Y];
    // }

    // public void ExecuteAbility(Actor selectedActor, string inputAction, List<Vector2> target, Spectrum spectrum)
    // {
    //     selectedActor.ExecuteAbility(inputAction, selectedActor, this, target, spectrum);
    // }
}
