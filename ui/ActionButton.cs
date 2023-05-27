using Godot;
using System;

public partial class ActionButton : Control
{
    private Action action;

    public override void _Ready()
    {
        GetNode<Button>("Button").Pressed += this.OnButtonPressed;
    }

    public void Setup(Action action)
    {
        this.action = action;
        GetNode<Button>("Button").Text = action.DisplayName;
    }

    public void OnButtonPressed()
    {
        GetParent<UI>().HandleActionButtonClick(action.InputAction);
    }
}
