using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnOrder : Control
{
    private List<Actor> actors;
    private int currentSpeedValue = 0;
    private int currentTurnOrderIndex = 0;
    private List<Actor> turnOrder = new List<Actor>();

    private Action<Actor> currentActorCallback;

    public override void _Ready()
    {
        GetNode<Button>("EndTurnButton").Pressed += this.OnEndTurnButtonPressed;
    }

    public void Setup(List<Actor> actors, Action<Actor> actorCallback)
    {
        this.actors = actors;
        this.currentActorCallback = actorCallback;
        this.calculateTurnOrder();
        this.SetTurnOrder(this.getDisplayTurnOrder());
        currentActorCallback(turnOrder[currentTurnOrderIndex]);
    }

    public void OnEndTurnButtonPressed()
    {
        currentSpeedValue += turnOrder[currentTurnOrderIndex].Speed;
        currentTurnOrderIndex++;
        this.calculateTurnOrder();
        this.SetTurnOrder(this.getDisplayTurnOrder());
        currentActorCallback(turnOrder[currentTurnOrderIndex]);
    }

    public void SetTurnOrder(List<Actor> actors)
    {
        for (int i = 0; i < 8; i++)
        {
            TextureRect actorImage = GetNode<TextureRect>("Actor" + (i + 1));
            if (i < actors.Count)
            {
                actorImage.Texture = actors[i].Texture;
                actorImage.Show();
            }
            else
            {
                actorImage.Hide();
            }
        }
    }

    public void ClearTurnOrder()
    {
        for (int i = 0; i < 8; i++)
        {
            Control actorImage = GetNode<Control>("Actor" + (i + 1));
            actorImage.Hide();
        }
    }

    private void calculateTurnOrder()
    {
        Dictionary<Actor, int> speedValues = new Dictionary<Actor, int>();

        int lowestSpeed = actors.MinBy(actor => actor.Speed).Speed;

        actors.ForEach(actor =>
        {
            speedValues[actor] = actor.Speed;
        });

        while (turnOrder.Count < 100)
        {
            Actor nextActor = speedValues.MinBy(kvp => kvp.Value).Key;
            turnOrder.Add(nextActor);
            speedValues[nextActor] = speedValues[nextActor] + nextActor.Speed;
        }
    }

    private List<Actor> getDisplayTurnOrder()
    {
        return turnOrder.ToArray().Skip(currentTurnOrderIndex).Take(8).ToList();
    }
}
