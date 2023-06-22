using Godot;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public partial class TurnOrderManager
{
    private List<Actor> actors;
    private int currentSpeedValue = 0;
    private int currentTurnOrderIndex = 0;
    private List<Actor> turnOrder = new List<Actor>();

    public TurnOrderManager(List<Actor> actors)
    {
        this.actors = actors;
        CalculateTurnOrder();
    }

    public void EndTurn()
    {
        currentSpeedValue += turnOrder[currentTurnOrderIndex].Speed;
        currentTurnOrderIndex++;
    }

    public void CalculateTurnOrder()
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

    public List<Actor> GetDisplayTurnOrder()
    {
        return turnOrder.ToArray().Skip(currentTurnOrderIndex).Take(8).ToList();
    }
}
