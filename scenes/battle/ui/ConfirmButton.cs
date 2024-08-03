using Godot;
using System;

public partial class ConfirmButton : TextureButton
{
	private Ability ability;

	public override void _Ready()
	{
		Pressed += this._onButtonPressed;
	}

	public void Setup(Ability ability)
	{
		this.ability = ability;
	}

	private void _onButtonPressed()
	{
		GetTree().CallGroup("InputReceivers", "_onAbilityConfirmButtonClicked", ability.InputAction);
	}
}
