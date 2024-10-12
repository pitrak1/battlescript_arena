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

        ActorTeam team1 = new ActorTeam(Colors.Red);
        ActorTeam team2 = new ActorTeam(Colors.Blue);

        Actor wolf = AddActor(ActorTypes.Wolf, new Vector2(5, 5), team1);
        // wolf.Effects.Add(new BleedEffect(wolf));
        wolf.Effects.Add(new IncreaseDamageTakenEffect(wolf));
        Actor turtle = AddActor(ActorTypes.Turtle, new Vector2(7, 7), team2);
        turtle.Effects.Add(new IncreaseDamageGivenEffect(turtle));
        // RemoveActor(wolf);

        Tile wolfTile = GetTileAtCoordinates(new Vector2(5, 5));
        wolfTile.Effects.Add(new IncreaseDamageTakenTileEffect(wolfTile));

        Tile turtleTile = GetTileAtCoordinates(new Vector2(7, 7));
        turtleTile.Effects.Add(new IncreaseDamageGivenTileEffect(turtleTile));

    }

    public void SetCurrentTile(Vector2 coordinates)
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (tiles[x, y] is not null)
                {
                    bool isCurrentTile = coordinates.X == x && coordinates.Y == y;
                    tiles[x, y].IsCurrent = isCurrentTile;
                }
            }
        }
    }

    public void SetSelectedTile(Vector2 coordinates)
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (tiles[x, y] is not null)
                {
                    bool isSelectedTile = coordinates.X == x && coordinates.Y == y;
                    tiles[x, y].IsSelected = isSelectedTile;
                }
            }
        }
    }

    public void ClearSelectedTile()
    {
        SetSelectedTile(new Vector2(-1, -1));
    }

    public void SetTargetedTiles(List<Vector2> targets)
    {
        ClearTargetedTiles();
        targets.ForEach(target => GetTileAtCoordinates(target).IsTargeted = true);
    }

    public void ClearTargetedTiles()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (tiles[x, y] is not null)
                {
                    tiles[x, y].IsTargeted = false;
                }
            }
        }
    }

    public Actor AddActor(ActorTypes actorType, Vector2 coordinates, ActorTeam team)
    {
        Actor actor = actorScene.Instantiate<Actor>();
        actor.Setup(actorType, coordinates, team);
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
}
