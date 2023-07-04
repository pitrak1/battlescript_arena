using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    private World world;
    private AbilityButtons abilityButtons;
    private TurnOrder turnOrder;

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

        Actor wolfActor = ActorConfig.ActorSceneMap[ActorTypes.Wolf].Instantiate<Actor>();
        world.PlaceActor(wolfActor, new Vector2(3, 3));
        actors.Add(wolfActor);


        Actor turtleActor = ActorConfig.ActorSceneMap[ActorTypes.Turtle].Instantiate<Actor>();
        world.PlaceActor(turtleActor, new Vector2(6, 5));
        actors.Add(turtleActor);

        turnOrder.Setup(actors, HandleSetCurrentActor);
    }

    private void HandleSetCurrentActor(Actor actor)
    {
        currentActor = actor;
        updateAbilityButtons();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        string[] validKeys = { "Q", "W", "E" };
        foreach (string key in validKeys)
        {
            if (Input.IsActionJustPressed(key))
            {
                HandleInput(key);
            }
        }
    }

    public void HandleAbilityButtonClick(string key)
    {
        HandleInput(key);
    }

    public void HandleAbilityConfirmButtonClick(string key)
    {
        HandleConfirm(key);
    }

    public void HandleTileClick(Vector2 coords)
    {
        Actor selectedActor = world.HandleTileClick(coords);
        HandleTileClick(coords, selectedActor);
    }
}
