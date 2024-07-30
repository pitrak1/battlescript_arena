using Godot;
using System;

public partial class AbilityButton : Control
{
    private Ability ability;

    public override void _Ready()
    {
        GetNode<Button>("Button").Pressed += this._onButtonPressed;
        GetNode<Button>("ConfirmButton").Pressed += this._onConfirmButtonPressed;
    }

    public void Setup(Ability ability)
    {
        this.ability = ability;
        GetNode<Button>("Button").Text = ability.DisplayName;
        GetNode<Button>("ConfirmButton").Visible = false;
    }

    public void ShowConfirmButton()
    {
        GetNode<Button>("ConfirmButton").Visible = true;
    }

    public void HideConfirmButton()
    {
        GetNode<Button>("ConfirmButton").Visible = false;
    }

    private void _onButtonPressed()
    {
        GetTree().CallGroup("InputReceivers", "_onAbilityButtonClicked", ability.InputAction);
    }

    private void _onConfirmButtonPressed()
    {
        GetTree().CallGroup("InputReceivers", "_onAbilityConfirmButtonClicked", ability.InputAction);
    }
}
