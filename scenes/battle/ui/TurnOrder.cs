using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnOrder : Control
{
    private List<Actor> turnOrder = new List<Actor>();

    public override void _Ready()
    {
        GetNode<Button>("EndTurnButton").Pressed += this.onEndTurnButtonClicked;
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
        while (turnOrder.Count < 100)
        {
            Actor nextActor = speedValues.MinBy(kvp => kvp.Value).Key;
            turnOrder.Add(nextActor);
            speedValues[nextActor] = speedValues[nextActor] + nextActor.Speed;
        }

        displayTurnOrder();
    }

    private void displayTurnOrder()
    {
        for (int i = 0; i < 8; i++)
        {
            TextureRect actorImage = GetNode<TextureRect>("Actor" + (i + 1));
            if (i < turnOrder.Count)
            {
                actorImage.Texture = turnOrder[i].Texture;
                actorImage.Show();
            }
            else
            {
                actorImage.Hide();
            }
        }
    }

    public void GoToNextActor()
    {
        turnOrder.RemoveAt(0);
        displayTurnOrder();
    }
}
