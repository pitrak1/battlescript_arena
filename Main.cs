using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    private World world;
    private UI ui;

    private BattleStateMachine battleStateMachine;

    private Vector2 selectedCoords;
    private Actor selectedActor;
    private string selectedAction;

    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();

        ui = GetNode<UI>("UI");

        battleStateMachine = new BattleStateMachine(HandleExecuteAbility, HandleSetAbilities);
    }

    private void HandleExecuteAbility(List<Vector2> coords, Actor actor, string action)
    {
        world.ExecuteAbility(action, coords);
    }

    private void HandleSetAbilities(Actor actor)
    {
        if (actor == null)
        {
            ui.ClearAbilities();
        }
        else
        {
            ui.SetAbilities(actor.GetAbilities());
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        string[] validKeys = { "Q", "W" };
        foreach (string key in validKeys)
        {
            if (Input.IsActionJustPressed(key))
            {
                battleStateMachine.HandleInput(key);
            }
        }
    }

    public void HandleAbilityButtonClick(string key)
    {
        battleStateMachine.HandleInput(key);
    }

    public void HandleTileClick(Vector2 coords)
    {
        Actor selectedActor = world.HandleTileClick(coords);
        battleStateMachine.HandleTileClick(coords, selectedActor);
    }
}
