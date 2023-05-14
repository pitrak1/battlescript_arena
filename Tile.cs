using Godot;
using System.Collections.Generic;

public partial class Tile : Sprite2D
{
    private Vector2 offset = new Vector2(400, 200);

    public Vector2 coordinates;
    private Actor actor;

    public virtual void Setup(Vector2 coords)
    {
        this.coordinates = coords;
        this.Position = new Vector2((coords.X - (coords.Y * 0.5f)) * 64, coords.Y * 43) + offset;
    }

    public void Highlight(bool isHighlighted)
    {
        GetNode<Sprite2D>("HighlightSprite").Visible = isHighlighted;
    }

    public Actor GetActor()
    {
        return actor;
    }

    public void PlaceActor(Actor actor)
    {
        actor.Place(this.coordinates);
        actor.Position = new Vector2(0, 0);
        AddChild(actor);
        this.actor = actor;
    }

    public Actor RemoveActor()
    {
        Actor actor = this.actor;
        this.actor = null;
        RemoveChild(actor);
        return actor;
    }

    public void _on_static_body_2d_input_event(Node viewport, InputEvent inputEvent, int shape_idx)
    {
        if (Input.IsActionJustPressed("LMB"))
        {
            GetParent<World>().GetParent<Main>().HandleTileClick(coordinates);
        }
    }
}
