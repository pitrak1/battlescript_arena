using Godot;
using System.Collections.Generic;
using System.Linq;
using System;

public enum AbilitySelectStates
{
    None,
    ActorSelected,
    AbilitySelected,
    Confirm,
}

public class BattleStateMachine
{
    public AbilitySelectStates AbilitySelectState { get; private set; } = AbilitySelectStates.None;
    public List<Vector2> SelectedCoords { get; private set; } = new List<Vector2>();
    public Actor SelectedActor { get; private set; }
    public string SelectedAbility { get; private set; }
    private Action<Actor> setActionsCallback;
    private Action<string> abilityReadyCallback;
    private Actor currentActor;

    private Action<List<Vector2>, Actor, string> actionExecuteCallback;

    public BattleStateMachine(Action<List<Vector2>, Actor, string> actionExecuteCallback, Action<Actor> setActionsCallback, Action<string> abilityReadyCallback)
    {
        this.actionExecuteCallback = actionExecuteCallback;
        this.setActionsCallback = setActionsCallback;
        this.abilityReadyCallback = abilityReadyCallback;
    }

    public void HandleInput(string actionPressed)
    {
        if (AbilitySelectState == AbilitySelectStates.ActorSelected)
        {
            string[] validKeys = { "Q", "W", "E" };
            if (validKeys.Contains(actionPressed))
            {
                AbilitySelectState = AbilitySelectStates.AbilitySelected;
                SelectedAbility = actionPressed;
                evaluateNumberOfTargets();
            }
        }
        else if (AbilitySelectState == AbilitySelectStates.Confirm)
        {
            HandleConfirm(actionPressed);
        }
    }

    public void HandleConfirm(string actionPressed)
    {
        if (AbilitySelectState == AbilitySelectStates.Confirm)
        {
            string[] validKeys = { "Q", "W", "E" };
            if (validKeys.Contains(actionPressed))
            {
                actionExecuteCallback(SelectedCoords, SelectedActor, SelectedAbility);
                SelectedCoords = new List<Vector2>();
                SelectedAbility = null;
                AbilitySelectState = AbilitySelectStates.ActorSelected;
            }
        }
    }

    public void HandleTileClick(Vector2 coords, Actor selectedActor)
    {

        if (AbilitySelectState == AbilitySelectStates.None || AbilitySelectState == AbilitySelectStates.ActorSelected)
        {
            SelectedActor = selectedActor;

            if (selectedActor != null && selectedActor == currentActor)
            {
                AbilitySelectState = AbilitySelectStates.ActorSelected;
                setActionsCallback(selectedActor);
            }
            else
            {
                AbilitySelectState = AbilitySelectStates.None;
                setActionsCallback(null);
            }
        }
        else if (AbilitySelectState == AbilitySelectStates.AbilitySelected)
        {
            SelectedCoords.Add(coords);
            evaluateNumberOfTargets();
        }
    }

    public void SetCurrentActor(Actor actor)
    {
        currentActor = actor;

        if (SelectedActor != null && SelectedActor == currentActor)
        {
            AbilitySelectState = AbilitySelectStates.ActorSelected;
            setActionsCallback(SelectedActor);
        }
        else
        {
            AbilitySelectState = AbilitySelectStates.None;
            setActionsCallback(null);
        }
    }

    private void evaluateNumberOfTargets()
    {
        int expectedTargetCount = SelectedActor.GetAbilities().Find(ab => ab.InputAction == SelectedAbility).NumberOfTargets;
        if (SelectedCoords.Count >= expectedTargetCount)
        {
            AbilitySelectState = AbilitySelectStates.Confirm;
            this.abilityReadyCallback(SelectedAbility);
        }
    }
}
