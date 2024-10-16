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
            TextureButton abilityButton = GetNode<TextureButton>("AbilityButton" + (i + 1));
            abilityButtons.Add(abilityButton);
            abilityButton.Hide();

            TextureButton confirmButton = GetNode<TextureButton>("ConfirmButton" + (i + 1));
            confirmButtons.Add(confirmButton);
            confirmButton.Hide();

            abilityHandlers.Add(null);
            confirmHandlers.Add(null);
        }
    }

    public void SetAbilities(List<Ability> abilities)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilityButtons[i].TextureNormal = GD.Load<Texture2D>(abilities[i].IconAsset);
            abilityButtons[i].Show();

            // Remove the existing handler if present, store our new one, and connect it
            if (abilityHandlers[i] is not null) {
                abilityButtons[i].Pressed -= abilityHandlers[i];
            }

            Action abilityHandler = () => _onAbilityButtonPressed(abilities[i].InputAction);
            abilityButtons[i].Pressed += abilityHandler;
            abilityHandlers[i] = abilityHandler;

            // Remove the existing handler if present, store our new one, and connect it
            if (confirmHandlers[i] is not null) {
                confirmButtons[i].Pressed -= confirmHandlers[i];
            }

            Action confirmHandler = () => _onConfirmButtonPressed(abilities[i].InputAction);
            confirmButtons[i].Pressed += confirmHandler;
            confirmHandlers[i] = confirmHandler;
        }
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
        confirmButtons[index].Show();
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
