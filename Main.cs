using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    private World world;
    private AbilityButtons abilityButtons;
    private TurnOrder turnOrder;

    private BattleStateMachine battleStateMachine;
    private TurnOrderManager turnOrderManager;

    private Vector2 selectedCoords;
    private Actor selectedActor;
    private string selectedAction;

    private List<Actor> actors = new List<Actor>();

    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();

        abilityButtons = GetNode<AbilityButtons>("AbilityButtons");
        this.turnOrder = GetNode<TurnOrder>("TurnOrder");

        battleStateMachine = new BattleStateMachine(HandleExecuteAbility, HandleSetAbilities);

        Actor wolfActor = ActorConfig.ActorSceneMap[ActorTypes.Wolf].Instantiate<Actor>();
        world.PlaceActor(wolfActor, new Vector2(3, 3));
        wolfActor.AddAbility(new MoveAbility("Q"));
        wolfActor.AddAbility(new HurtSelfAbility("W"));
        wolfActor.SetMaxHealth(15);
        actors.Add(wolfActor);


        Actor turtleActor = ActorConfig.ActorSceneMap[ActorTypes.Turtle].Instantiate<Actor>();
        world.PlaceActor(turtleActor, new Vector2(6, 5));
        turtleActor.AddAbility(new MoveAbility("Q"));
        turtleActor.AddAbility(new HurtSelfAbility("W"));
        turtleActor.SetMaxHealth(20);
        actors.Add(turtleActor);

        turnOrderManager = new TurnOrderManager(actors);
        List<Actor> currentTurnOrder = turnOrderManager.GetDisplayTurnOrder();
        this.turnOrder.SetTurnOrder(currentTurnOrder);
    }

    private void HandleExecuteAbility(List<Vector2> coords, Actor actor, string action)
    {
        world.ExecuteAbility(actor, action, coords);
    }

    private void HandleSetAbilities(Actor actor)
    {
        if (actor == null)
        {
            abilityButtons.ClearAbilities();
        }
        else
        {
            abilityButtons.SetAbilities(actor.GetAbilities());
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

    public void HandleEndTurnButtonClick()
    {
        turnOrderManager.EndTurn();
        turnOrder.SetTurnOrder(turnOrderManager.GetDisplayTurnOrder());
    }

    public void HandleTileClick(Vector2 coords)
    {
        Actor selectedActor = world.HandleTileClick(coords);
        battleStateMachine.HandleTileClick(coords, selectedActor);
    }
}
