using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    private World world;
    private UI ui;

    private BattleStateMachine battleStateMachine;
    private TurnOrderManager turnOrderManager;

    private Vector2 selectedCoords;
    private Actor selectedActor;
    private string selectedAction;

    private List<Actor> actors = new List<Actor>();

    private PackedScene actorScene = GD.Load<PackedScene>("res://actors/Actor.tscn");
    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();

        ui = GetNode<UI>("UI");

        battleStateMachine = new BattleStateMachine(HandleExecuteAbility, HandleSetAbilities);
        turnOrderManager = new TurnOrderManager();


        Actor wolfActor = actorScene.Instantiate<Actor>();
        wolfActor.SetType(ActorType.Wolf);
        world.PlaceActor(wolfActor, new Vector2(3, 3));
        wolfActor.AddAbility(new MoveAbility("Q"));
        wolfActor.AddAbility(new HurtSelfAbility("W"));
        wolfActor.SetMaxHealth(15);
        actors.Add(wolfActor);


        Actor turtleActor = actorScene.Instantiate<Actor>();
        turtleActor.SetType(ActorType.Turtle);
        world.PlaceActor(turtleActor, new Vector2(6, 5));
        turtleActor.AddAbility(new MoveAbility("Q"));
        turtleActor.AddAbility(new HurtSelfAbility("W"));
        turtleActor.SetMaxHealth(20);
        actors.Add(turtleActor);

        List<Actor> turnOrder = turnOrderManager.GetTurnOrder(new List<Actor> { wolfActor, turtleActor });
        ui.SetTurnOrder(turnOrder);
    }

    private void HandleExecuteAbility(List<Vector2> coords, Actor actor, string action)
    {
        world.ExecuteAbility(actor, action, coords);
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
