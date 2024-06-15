using Godot;
using System.Collections.Generic;

public enum ActorTypes
{
    Wolf,
    Turtle
}

public partial class ActorConfig
{
    public static Dictionary<ActorTypes, PackedScene> ActorSceneMap = new Dictionary<ActorTypes, PackedScene>() {
        {ActorTypes.Wolf, GD.Load<PackedScene>("res://actors/WolfActor.tscn")},
        {ActorTypes.Turtle, GD.Load<PackedScene>("res://actors/TurtleActor.tscn")},
    };
}