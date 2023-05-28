using Godot;
using System;
using System.Collections.Generic;

public enum ActionSelectStates
{
    None,
    ActorSelected,
    AbilitySelected,
    Execution,
}

public partial class Main : Node
{
    private World world;
    private UI ui;

    private ActionSelectStates actionSelectState = ActionSelectStates.None;
    private Vector2 selectedCoords;
    private Actor selectedActor;
    private string selectedAction;

    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();

        ui = GetNode<UI>("UI");
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (actionSelectState == ActionSelectStates.ActorSelected)
        {
            if (Input.IsActionJustPressed("Q"))
            {
                actionSelectState = ActionSelectStates.AbilitySelected;
                selectedAction = "Q";
            }
            else if (Input.IsActionJustPressed("W"))
            {
                actionSelectState = ActionSelectStates.AbilitySelected;
                selectedAction = "W";
            }
        }

    }

    public void HandleActionButtonClick(string key)
    {
        if (actionSelectState == ActionSelectStates.ActorSelected)
        {
            actionSelectState = ActionSelectStates.AbilitySelected;
            selectedAction = key;
        }

    }

    public void HandleTileClick(Vector2 coords)
    {
        Actor clickedActor = world.HandleTileClick(coords);
        if (actionSelectState == ActionSelectStates.None || actionSelectState == ActionSelectStates.ActorSelected)
        {
            this.selectedCoords = coords;
            selectedActor = clickedActor;
            if (selectedActor != null)
            {
                actionSelectState = ActionSelectStates.ActorSelected;
                ui.SetActions(selectedActor.GetActions());

            }
            else
            {
                actionSelectState = ActionSelectStates.None;
                ui.ClearActions();
            }
        }
        else if (actionSelectState == ActionSelectStates.AbilitySelected)
        {
            actionSelectState = ActionSelectStates.ActorSelected;
            world.ExecuteAction(selectedAction, coords);
            selectedAction = null;
        }
    }
}
