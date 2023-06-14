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
    public Vector2 SelectedCoords { get; private set; }
    public Actor SelectedActor { get; private set; }
    public string SelectedAbility { get; private set; }
    private Action<Actor> setActionsCallback;

    private Action<Vector2, Actor, string> actionExecuteCallback;

    public BattleStateMachine(Action<Vector2, Actor, string> actionExecuteCallback, Action<Actor> setActionsCallback)
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
            }
        }
    }

    public void HandleTileClick(Vector2 coords, Actor selectedActor)
    {
        if (AbilitySelectState == AbilitySelectStates.None || AbilitySelectState == AbilitySelectStates.ActorSelected)
        {
            SelectedCoords = coords;
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
            AbilitySelectState = AbilitySelectStates.ActorSelected;
            actionExecuteCallback(coords, SelectedActor, SelectedAbility);
        }
    }
}
