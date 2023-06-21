using Godot;
using System;
using System.Collections.Generic;

public partial class TurnOrder : Control
{
    private Vector2 basePosition;
    public override void _Ready()
    {
        this.ClearTurnOrder();
        Vector2 viewportSize = GetNode<Control>("Actor1").GetViewportRect().Size;
        basePosition = new Vector2(viewportSize.X / 2 - 40, 0);
    }

    public void SetTurnOrder(List<Actor> actors)
    {
        for (int i = 0; i < 8; i++)
        {
            TextureRect actorImage = GetNode<TextureRect>("Actor" + (i + 1));
            if (i < actors.Count)
            {
                int xOffset = ((actors.Count - 1) * -50) + 100 * i;
                actorImage.Position = basePosition + new Vector2(xOffset, 0);
                actorImage.Texture = actors[i].Texture;
                actorImage.Show();
            }
            else
            {
                actorImage.Hide();
            }
        }
    }

    public void ClearTurnOrder()
    {
        for (int i = 0; i < 8; i++)
        {
            Control actorImage = GetNode<Control>("Actor" + (i + 1));
            actorImage.Hide();
        }
    }
}
