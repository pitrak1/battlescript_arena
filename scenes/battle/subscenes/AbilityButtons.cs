using Godot;
using System;
using System.Collections.Generic;

public partial class AbilityButtons : Control
{
    private List<AbilityButton> abilityButtons = new List<AbilityButton>();
    private List<ConfirmButton> confirmButtons = new List<ConfirmButton>();

    public override void _Ready()
    {
        for (int i = 0; i < 5; i++)
        {
            AbilityButton abilityButton = GetNode<AbilityButton>("AbilityButton" + (i + 1));
            abilityButtons.Add(abilityButton);
            abilityButton.Hide();
        }

        for (int i = 0; i < 5; i++)
        {
            ConfirmButton confirmButton = GetNode<ConfirmButton>("ConfirmButton" + (i + 1));
            confirmButtons.Add(confirmButton);
            confirmButton.Hide();
        }
    }

    public void SetAbilities(List<Ability> abilities)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            abilityButtons[i].Setup(abilities[i]);
            abilityButtons[i].Show();

            confirmButtons[i].Setup(abilities[i]);
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
}
