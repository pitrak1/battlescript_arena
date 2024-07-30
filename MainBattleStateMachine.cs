using Godot;
using System.Collections.Generic;
using System.Linq;
using System;

// public enum AbilitySelectStates
// {
//     None,
//     ActorSelected,
//     AbilitySelected,
//     Confirm,
// }

public partial class Main : Node
{
    // public AbilitySelectStates AbilitySelectState { get; private set; } = AbilitySelectStates.None;
    // public List<Vector2> SelectedCoords { get; private set; } = new List<Vector2>();
    // public Actor SelectedActor { get; private set; }
    // public string SelectedAbility { get; private set; }

    // public void HandleInput(string actionPressed)
    // {
    //     if (AbilitySelectState == AbilitySelectStates.ActorSelected)
    //     {
    //         string[] validKeys = { "Q", "W", "E" };
    //         if (validKeys.Contains(actionPressed))
    //         {
    //             AbilitySelectState = AbilitySelectStates.AbilitySelected;
    //             SelectedAbility = actionPressed;
    //             evaluateNumberOfTargets();
    //         }
    //     }
    //     else if (AbilitySelectState == AbilitySelectStates.Confirm)
    //     {
    //         HandleConfirm(actionPressed);
    //     }
    // }

    // public void HandleConfirm(string actionPressed)
    // {
    //     if (AbilitySelectState == AbilitySelectStates.Confirm)
    //     {
    //         string[] validKeys = { "Q", "W", "E" };
    //         if (validKeys.Contains(actionPressed))
    //         {
    //             world.ExecuteAbility(SelectedActor, SelectedAbility, SelectedCoords, spectrum);
    //             abilityButtons.HideConfirmButtons();
    //             SelectedCoords = new List<Vector2>();
    //             SelectedAbility = null;
    //             AbilitySelectState = AbilitySelectStates.ActorSelected;
    //         }
    //     }
    // }

    // public void HandleTileClick(Vector2 coords, Actor selectedActor)
    // {
    //     if (AbilitySelectState == AbilitySelectStates.None || AbilitySelectState == AbilitySelectStates.ActorSelected)
    //     {
    //         SelectedActor = selectedActor;
    //         updateAbilityButtons();
    //     }
    //     else if (AbilitySelectState == AbilitySelectStates.AbilitySelected)
    //     {
    //         SelectedCoords.Add(coords);
    //         evaluateNumberOfTargets();
    //     }
    // }

    // private void updateAbilityButtons()
    // {
    //     if (SelectedActor != null && SelectedActor == currentActor)
    //     {
    //         AbilitySelectState = AbilitySelectStates.ActorSelected;
    //         abilityButtons.SetAbilities(SelectedActor.GetAbilities());
    //     }
    //     else
    //     {
    //         AbilitySelectState = AbilitySelectStates.None;
    //         abilityButtons.ClearAbilities();
    //     }
    // }

    // private void evaluateNumberOfTargets()
    // {
    //     int expectedTargetCount = SelectedActor.GetAbilities().Find(ab => ab.InputAction == SelectedAbility).NumberOfTargets;
    //     if (SelectedCoords.Count >= expectedTargetCount)
    //     {
    //         AbilitySelectState = AbilitySelectStates.Confirm;
    //         Dictionary<string, int> keyMap = new Dictionary<string, int>() { { "Q", 0 }, { "W", 1 }, { "E", 2 } };
    //         abilityButtons.ShowConfirmButton(keyMap[SelectedAbility]);
    //     }
    // }
}
