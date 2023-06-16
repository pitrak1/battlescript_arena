using Godot;
using System.Collections.Generic;
using System.Linq;
using System;

public enum AbilitySelectStates
{
    None,
    ActorSelected,
    AbilitySelected,
    Execution,
}

public class BattleStateMachine
{
    public AbilitySelectStates AbilitySelectState { get; private set; } = AbilitySelectStates.None;
    public List<Vector2> SelectedCoords { get; private set; } = new List<Vector2>();
    public Actor SelectedActor { get; private set; }
    public string SelectedAbility { get; private set; }
    private Action<Actor> setActionsCallback;

    private Action<List<Vector2>, Actor, string> actionExecuteCallback;

    public BattleStateMachine(Action<List<Vector2>, Actor, string> actionExecuteCallback, Action<Actor> setActionsCallback)
    {
        this.actionExecuteCallback = actionExecuteCallback;
        this.setActionsCallback = setActionsCallback;
    }

    public void HandleInput(string actionPressed)
    {
        if (AbilitySelectState == AbilitySelectStates.ActorSelected)
        {
            string[] validKeys = { "Q", "W" };
            if (validKeys.Contains(actionPressed))
            {
                AbilitySelectState = AbilitySelectStates.AbilitySelected;
                SelectedAbility = actionPressed;
                evaluateNumberOfTargets();
            }
        }
    }

    public void HandleTileClick(Vector2 coords, Actor selectedActor)
    {

        if (AbilitySelectState == AbilitySelectStates.None || AbilitySelectState == AbilitySelectStates.ActorSelected)
        {
            SelectedActor = selectedActor;

            if (selectedActor != null)
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

    private void evaluateNumberOfTargets()
    {
        int expectedTargetCount = SelectedActor.GetAbilities().Find(ab => ab.InputAction == SelectedAbility).NumberOfTargets;
        if (SelectedCoords.Count >= expectedTargetCount)
        {
            actionExecuteCallback(SelectedCoords, SelectedActor, SelectedAbility);
            SelectedCoords = new List<Vector2>();
            SelectedAbility = null;
            AbilitySelectState = AbilitySelectStates.ActorSelected;
        }
    }
}
