using Godot;
using System;
using System.Collections.Generic;

public partial class TurnOrder : Control
{
    private Vector2 basePosition;
    public override void _Ready()
    {
        this.ClearTurnOrder();
        GetNode<Button>("EndTurnButton").Pressed += this.OnEndTurnButtonPressed;
    }

    public void OnEndTurnButtonPressed()
    {
        GetParent<UI>().HandleEndTurnButtonClick();
    }

    public void SetTurnOrder(List<Actor> actors)
    {
        for (int i = 0; i < 8; i++)
        {
            TextureRect actorImage = GetNode<TextureRect>("Actor" + (i + 1));
            if (i < actors.Count)
            {
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
