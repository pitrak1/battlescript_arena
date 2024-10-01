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

    public List<Effect> Effects { get; private set; } = new List<Effect>();

    private TileTypes type;

    private Sprite2D currentIndicatorSprite;
    private bool isCurrent;
    public bool IsCurrent
    {
        get { return isCurrent; }
        set
        {
            isCurrent = value;
            currentIndicatorSprite.Visible = value;
        }
    }

    private Sprite2D targetIndicatorSprite;
    private bool isTargeted;
    public bool IsTargeted
    {
        get { return isTargeted; }
        set
        {
            isTargeted = value;
            targetIndicatorSprite.Visible = value;
        }
    }

    private Sprite2D selectIndicatorSprite;
    private bool isSelected;
    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            isSelected = value;
            selectIndicatorSprite.Visible = value;
        }
    }


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

        this.currentIndicatorSprite = GetNode<Sprite2D>("CurrentIndicatorSprite");
        this.targetIndicatorSprite = GetNode<Sprite2D>("TargetIndicatorSprite");
        this.selectIndicatorSprite = GetNode<Sprite2D>("SelectIndicatorSprite");

        IsCurrent = false;
        IsSelected = false;
        IsTargeted = false;
    }

    public void _on_static_body_2d_input_event(Node viewport, InputEvent inputEvent, int shape_idx)
    {
        if (Input.IsActionJustPressed("LMB"))
        {
            GetTree().CallGroup("InputReceivers", "_onTileClicked", coordinates);
        }
    }
}
