using Godot;
using System.Collections.Generic;

public partial class Tile : Sprite2D
{
    private SortedDictionary<TileType, Texture2D> textureMap = new SortedDictionary<TileType, Texture2D> {
        {TileType.Low, GD.Load<Texture2D>("res://assets/low.png")},
        {TileType.Middle, GD.Load<Texture2D>("res://assets/middle.png")},
        {TileType.High, GD.Load<Texture2D>("res://assets/high.png")},
        {TileType.Rock, GD.Load<Texture2D>("res://assets/rock.png")},
        {TileType.Tree, GD.Load<Texture2D>("res://assets/tree.png")},
        {TileType.Water, GD.Load<Texture2D>("res://assets/water.png")},
        {TileType.None, null}
    };

    private Vector2 offset = new Vector2(400, 200);

    public Vector2 coordinates;
    private Actor actor;

    public void Setup(TileType type, Vector2 coords)
    {
        this.Texture = textureMap[type];
        this.coordinates = coords;
        this.Position = new Vector2((coords.X - (coords.Y * 0.5f)) * 64, coords.Y * 43) + offset;
    }

    public void Highlight(bool isHighlighted)
    {
        GetNode<Sprite2D>("HighlightSprite").Visible = isHighlighted;
    }

    public void PlaceActor(Actor actor)
    {
        actor.Place(this.coordinates);
        actor.Position = new Vector2(0, 0);
        AddChild(actor);
        this.actor = actor;
    }

    public void _on_static_body_2d_input_event(Node viewport, InputEvent inputEvent, int shape_idx)
    {
        if (Input.IsActionJustPressed("LMB"))
        {
            GetParent<World>().GetParent<Main>().HandleTileClick(coordinates);
        }
    }
}
