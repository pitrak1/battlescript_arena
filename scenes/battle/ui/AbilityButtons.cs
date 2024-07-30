using Godot;
using System;
using System.Collections.Generic;

public partial class AbilityButtons : Control
{
    private Vector2 basePosition;
    public override void _Ready()
    {
        ClearAbilities();
        Vector2 viewportSize = GetNode<Control>("AbilityButton1").GetViewportRect().Size;
        basePosition = new Vector2(viewportSize.X / 2 - 40, 0);
    }

    public void SetAbilities(List<Ability> abilities)
    {
        for (int i = 0; i < 6; i++)
        {
            AbilityButton abilityButton = GetNode<AbilityButton>("AbilityButton" + (i + 1));
            if (i < abilities.Count)
            {
                int xOffset = ((abilities.Count - 1) * -50) + 100 * i;
                abilityButton.Position = basePosition + new Vector2(xOffset, 0);
                abilityButton.Setup(abilities[i]);
                abilityButton.Show();
            }
            else
            {
                abilityButton.Hide();
            }
        }
    }

    public void ClearAbilities()
    {
        for (int i = 0; i < 6; i++)
        {
            Control abilityButton = GetNode<Control>("AbilityButton" + (i + 1));
            abilityButton.Hide();
        }
    }

    public void ShowConfirmButton(int index)
    {
        AbilityButton abilityButton = GetNode<AbilityButton>("AbilityButton" + (index + 1));
        abilityButton.ShowConfirmButton();
    }

    public void HideConfirmButtons()
    {
        for (int i = 0; i < 6; i++)
        {
            AbilityButton abilityButton = GetNode<AbilityButton>("AbilityButton" + (i + 1));
            abilityButton.HideConfirmButton();
        }
    }
}
