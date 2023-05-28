using Godot;
using System;
using System.Collections.Generic;

public partial class UI : Control
{
    private Vector2 basePosition;
    public override void _Ready()
    {
        this.ClearActions();
        Vector2 viewportSize = GetNode<Control>("ActionButton1").GetViewportRect().Size;
        basePosition = new Vector2(viewportSize.X / 2 - 40, 0);
    }

    public void SetActions(List<Action> actions)
    {
        for (int i = 0; i < 6; i++)
        {
            ActionButton actionButton = GetNode<ActionButton>("ActionButton" + (i + 1));
            if (i < actions.Count)
            {
                int xOffset = ((actions.Count - 1) * -50) + 100 * i;
                actionButton.Position = basePosition + new Vector2(xOffset, 0);
                actionButton.Setup(actions[i]);
                actionButton.Show();
            }
            else
            {
                actionButton.Hide();
            }
        }
    }

    public void ClearActions()
    {
        for (int i = 0; i < 6; i++)
        {
            Control actionButton = GetNode<Control>("ActionButton" + (i + 1));
            actionButton.Hide();
        }
    }

    public void HandleActionButtonClick(string inputAction)
    {
        GetParent<Main>().HandleActionButtonClick(inputAction);
    }
}
