using Godot;
using System.Collections.Generic;

public partial class World : Node
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
    private Actor currentActor;

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
                TileTypes type = testLevel[x, y];
                Tile tile = TileConfig.TileSceneMap[type].Instantiate<Tile>();
                tile.Setup(new Vector2(x, y));
                AddChild(tile);
                tiles[x, y] = tile;
            }
        }
    }

    public void PlaceActor(Actor actor, Vector2 coordinates)
    {
        tiles[(int)coordinates.X, (int)coordinates.Y].PlaceActor(actor);
    }

    public Tile GetTile(Vector2 coordinates)
    {
        return tiles[(int)coordinates.X, (int)coordinates.Y];
    }

    public void ExecuteAbility(Actor selectedActor, string inputAction, List<Vector2> target, Spectrum spectrum)
    {
        selectedActor.ExecuteAbility(inputAction, selectedActor, this, target, spectrum);
    }
}
