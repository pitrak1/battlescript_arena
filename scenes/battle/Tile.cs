using Godot;
using System.Collections.Generic;

public enum TileTypes
{
    High,
    Middle,
    Low,
    Water,
    Rock,
    Tree,
    None
}

public partial class Tile : Sprite2D
{
    private Vector2 offset = new Vector2(400, 160);

    public Vector2 coordinates;
    private Actor currentActor;
    public Actor CurrentActor
    {
        get { return currentActor; }
        set
        {
            if (currentActor is not null)
            {
                RemoveChild(currentActor);
            }

            if (value is not null)
            {
                AddChild(value);
            }

            currentActor = value;
        }
    }

    private TileTypes type;

    private Dictionary<TileTypes, string> typeToAssetMap = new Dictionary<TileTypes, string>()
    {
        {TileTypes.High, "res://assets/high.png"},
        {TileTypes.Middle, "res://assets/middle.png"},
        {TileTypes.Low, "res://assets/low.png"},
        {TileTypes.Water, "res://assets/water.png"},
        {TileTypes.Rock, "res://assets/rock.png"},
        {TileTypes.Tree, "res://assets/tree.png"},
    };

    public virtual void Setup(Vector2 coords, TileTypes tileType)
    {
        this.coordinates = coords;
        this.Position = new Vector2((coords.X - (coords.Y * 0.5f)) * 64, coords.Y * 43) + offset;
        this.Texture = GD.Load<Texture2D>(typeToAssetMap[tileType]);
        this.type = tileType;
    }

    // This is simply to call the _OnTileClicked method in the BattleManager.
    // There were several ways to do this, but this seemed the most elegant.
    // We could have tunneled up the tree to the BattleManager node itself and emitted a signal,
    // but it felt brittle.
    // We also could be passing through the BattleManager node to here, but that seems really bad.
    public void _on_static_body_2d_input_event(Node viewport, InputEvent inputEvent, int shape_idx)
    {
        if (Input.IsActionJustPressed("LMB"))
        {
            GetTree().CallGroup("InputReceivers", "_OnTileClicked", coordinates);
        }
    }

    public void Highlight(bool isHighlighted)
    {
        GetNode<Sprite2D>("HighlightSprite").Visible = isHighlighted;
    }
}
