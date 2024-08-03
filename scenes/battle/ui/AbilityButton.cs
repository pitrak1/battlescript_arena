using Godot;
using System;

public partial class AbilityButton : TextureButton
{
    private Ability ability;

    public override void _Ready()
    {
        Pressed += this._onButtonPressed;
    }

    public void Setup(Ability ability)
    {
        this.ability = ability;
        this.TextureNormal = GD.Load<Texture2D>(ability.IconAsset);
    }

    private void _onButtonPressed()
    {
        GetTree().CallGroup("InputReceivers", "_onAbilityButtonClicked", ability.InputAction);
    }
}
