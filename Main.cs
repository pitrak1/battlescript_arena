using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    private World world;
    private AbilityButtons abilityButtons;
    private TurnOrder turnOrder;

    private BattleStateMachine battleStateMachine;

    private Vector2 selectedCoords;
    private Actor selectedActor;
    private string selectedAction;

    private Actor currentActor;

    private List<Actor> actors = new List<Actor>();

    private Spectrum spectrum;

    public override void _Ready()
    {
        world = (World)GetNode("World");
        world.Setup();

        spectrum = GetNode<Spectrum>("Spectrum");

        abilityButtons = GetNode<AbilityButtons>("AbilityButtons");
        this.turnOrder = GetNode<TurnOrder>("TurnOrder");

        battleStateMachine = new BattleStateMachine(HandleExecuteAbility, HandleSetAbilities, HandleAbilityReady);

        Actor wolfActor = ActorConfig.ActorSceneMap[ActorTypes.Wolf].Instantiate<Actor>();
        world.PlaceActor(wolfActor, new Vector2(3, 3));
        wolfActor.AddAbility(new MoveAbility("Q"));
        wolfActor.AddAbility(new HurtSelfAbility("W"));
        wolfActor.AddAbility(new InvokeEarthAbility("E"));
        wolfActor.SetMaxHealth(15);
        actors.Add(wolfActor);


        Actor turtleActor = ActorConfig.ActorSceneMap[ActorTypes.Turtle].Instantiate<Actor>();
        world.PlaceActor(turtleActor, new Vector2(6, 5));
        turtleActor.AddAbility(new MoveAbility("Q"));
        turtleActor.AddAbility(new HurtSelfAbility("W"));
        turtleActor.AddAbility(new ThrowDirtAbility("E"));
        turtleActor.SetMaxHealth(20);
        actors.Add(turtleActor);

        turnOrder.Setup(actors, HandleSetCurrentActor);
    }

    private void HandleAbilityReady(string action)
    {
        Dictionary<string, int> keyMap = new Dictionary<string, int>() { { "Q", 0 }, { "W", 1 }, { "E", 2 } };
        abilityButtons.ShowConfirmButton(keyMap[action]);
    }

    private void HandleExecuteAbility(List<Vector2> coords, Actor actor, string action)
    {
        world.ExecuteAbility(actor, action, coords, spectrum);
        abilityButtons.HideConfirmButtons();
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

    private void HandleSetCurrentActor(Actor actor)
    {
        currentActor = actor;
        battleStateMachine.SetCurrentActor(actor);
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        string[] validKeys = { "Q", "W", "E" };
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

    public void HandleAbilityConfirmButtonClick(string key)
    {
        battleStateMachine.HandleConfirm(key);
    }

    public void HandleTileClick(Vector2 coords)
    {
        Actor selectedActor = world.HandleTileClick(coords);
        battleStateMachine.HandleTileClick(coords, selectedActor);
    }
}
