using Godot;
using System.Collections.Generic;

public enum ActorType
{
    Wolf
}

public partial class Actor : Sprite2D
{
    private SortedDictionary<ActorType, Texture2D> textureMap = new SortedDictionary<ActorType, Texture2D> {
        {ActorType.Wolf, GD.Load<Texture2D>("res://assets/wolf.jpeg")},
    };

    public Vector2 Coordinates { get; set; }
    private List<Action> actions;

    public void Setup(ActorType type)
    {
        this.Texture = textureMap[type];
        this.actions = new List<Action>();
    }

    public void Place(Vector2 coords)
    {
        this.Coordinates = coords;
    }

    public void AddAction(Action action)
    {
        actions.Add(action);
    }

    public bool ExecuteAction(string inputAction, Actor source, World world, Vector2 target)
    {
        GD.Print(inputAction);
        var action = actions.Find(x => x.InputAction == inputAction);
        return action.Execute(source, world, target);
    }
}
