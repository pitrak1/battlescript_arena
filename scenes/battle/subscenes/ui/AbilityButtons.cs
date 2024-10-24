using Godot;
using System;
using System.Collections.Generic;

public partial class AbilityButtons : Control
{
    private List<TextureButton> abilityButtons = new List<TextureButton>();
    private List<TextureButton> confirmButtons = new List<TextureButton>();

    // Unfortunately, lambda expressions are not automatically freed when we associate a new handler
    // so we need to keep references and manually disconnect them
    private List<Action> abilityHandlers = new List<Action>();
    private List<Action> confirmHandlers = new List<Action>();
    
    public override void _Ready()
    {
        for (int i = 0; i < 5; i++)
        {
            TextureButton abilityButton = GetNode<TextureButton>("HBoxContainer/AbilityButton" + (i + 1) + "/AbilityTextureButton");
            abilityButtons.Add(abilityButton);
            abilityButton.Hide();

            TextureButton confirmButton = GetNode<TextureButton>("HBoxContainer/AbilityButton" + (i + 1) + "/ConfirmTextureButton");
            confirmButtons.Add(confirmButton);
            confirmButton.Hide();

            abilityHandlers.Add(null);
            confirmHandlers.Add(null);
        }
    }

    public void SetAbilities(List<Ability> abilities)
    {
        ClearAbilities();

        for (int i = 0; i < abilities.Count; i++)
        {
            setAbility(abilities[i], i);
        }
    }

    private void setAbility(Ability ability, int index)
    {
        abilityButtons[index].TextureNormal = GD.Load<Texture2D>(ability.IconAsset);
        abilityButtons[index].Show();

        // Remove the existing handler if present, store our new one, and connect it
        if (abilityHandlers[index] is not null) {
            abilityButtons[index].Pressed -= abilityHandlers[index];
        }

        Action abilityHandler = () => _onAbilityButtonPressed(ability.InputAction);
        abilityButtons[index].Pressed += abilityHandler;
        abilityHandlers[index] = abilityHandler;

        // Remove the existing handler if present, store our new one, and connect it
        if (confirmHandlers[index] is not null) {
            confirmButtons[index].Pressed -= confirmHandlers[index];
        }

        Action confirmHandler = () => _onConfirmButtonPressed(ability.InputAction);
        confirmButtons[index].Pressed += confirmHandler;
        confirmHandlers[index] = confirmHandler;
    }

    public void ClearAbilities()
    {
        for (int i = 0; i < abilityButtons.Count; i++)
        {
            abilityButtons[i].Hide();
            confirmButtons[i].Hide();
        }
    }

    public void ShowConfirmButton(int index)
    {
        if (index >= 0)
        {
            confirmButtons[index].Show();
        }
    }

    public void HideConfirmButtons()
    {
        for (int i = 0; i < confirmButtons.Count; i++)
        {
            confirmButtons[i].Hide();
        }
    }

    private void _onAbilityButtonPressed(string action) 
    {
        GetTree().CallGroup("InputReceivers", "_onAbilityButtonClicked", action);
    }

    private void _onConfirmButtonPressed(string action) 
    {
        GetTree().CallGroup("InputReceivers", "_onAbilityConfirmButtonClicked", action);
    }
}
