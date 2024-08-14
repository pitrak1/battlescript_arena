using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnOrder : Control
{
    public Actor CurrentActor { get; private set; }

    private TextureRect currentActorTexture;
    private TextureRect currentActorHighlightTexture;

    public List<Actor> CurrentTurnOrder { get; private set; } = new List<Actor>();

    private List<TextureRect> actorTextures = new List<TextureRect>();
    private List<TextureRect> actorHighlightTextures = new List<TextureRect>();

    public override void _Ready()
    {
        GetNode<Button>("EndTurnButton").Pressed += this.onEndTurnButtonClicked;

        currentActorTexture = GetNode<TextureRect>("CurrentActor");
        currentActorHighlightTexture = GetNode<TextureRect>("CurrentActor/Highlight");

        for (int i = 0; i < 8; i++)
        {
            actorTextures.Add(GetNode<TextureRect>("Actor" + (i + 1)));
            actorHighlightTextures.Add(GetNode<TextureRect>("Actor" + (i + 1) + "/Highlight"));
        }
    }

    private void onEndTurnButtonClicked()
    {
        GetTree().CallGroup("InputReceivers", "_onEndTurnButtonClicked");
    }

    public void SetTurnOrder(List<Actor> actors)
    {
        Dictionary<Actor, int> speedValues = new Dictionary<Actor, int>();
        int lowestSpeed = actors.MinBy(actor => actor.Speed).Speed;
        actors.ForEach(actor => { speedValues[actor] = actor.Speed; });

        // This works by having each actor have a speed value that tracks the current speed they've
        // used.  To get the next actor, we just need to find the minimum speed value, add its
        // actor to the turn order, and then add the speed of the actor to its speed value
        while (CurrentTurnOrder.Count < 100)
        {
            Actor nextActor = speedValues.MinBy(kvp => kvp.Value).Key;
            CurrentTurnOrder.Add(nextActor);
            speedValues[nextActor] = speedValues[nextActor] + nextActor.Speed;
        }

        GoToNextActor();
        updateDisplay();
    }

    private void updateDisplay()
    {
        currentActorTexture.Texture = CurrentActor.Texture;
        currentActorHighlightTexture.Modulate = CurrentActor.Team.TeamColor;

        for (int i = 0; i < 8; i++)
        {
            actorTextures[i].Texture = CurrentTurnOrder[i].Texture;
            actorHighlightTextures[i].Modulate = CurrentTurnOrder[i].Team.TeamColor;
        }
    }

    public void GoToNextActor()
    {
        CurrentActor = CurrentTurnOrder[0];
        CurrentTurnOrder.RemoveAt(0);
        updateDisplay();
    }
}