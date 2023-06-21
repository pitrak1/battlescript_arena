using Godot;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public partial class TurnOrderManager
{
    public List<Actor> GetTurnOrder(List<Actor> actors)
    {
        List<(Actor, int)> speedValues = new List<(Actor, int)>();

        actors.ForEach((actor) =>
        {
            for (int i = 0; i < 10; i++)
            {
                speedValues.Add((actor, actor.Speed * (i + 1)));
            }
        });

        speedValues = speedValues.ToArray().OrderBy(tuple => tuple.Item2).ToList();

        List<Actor> finalOrder = new List<Actor>();
        for (int i = 0; i < 8; i++)
        {
            finalOrder.Add(speedValues[i].Item1);
        }

        return finalOrder;
    }
}
