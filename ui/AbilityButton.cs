using Godot;
using System;

public partial class AbilityButton : Control
{
    private Ability ability;

    public override void _Ready()
    {
        GetNode<Button>("Button").Pressed += this.OnButtonPressed;
    }

    public void Setup(Ability ability)
    {
        this.ability = ability;
        GetNode<Button>("Button").Text = ability.DisplayName;
    }

    public void OnButtonPressed()
    {
        GetParent<UI>().HandleAbilityButtonClick(ability.InputAction);
    }
}
