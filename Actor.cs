using Godot;
using System.Collections.Generic;

public partial class Actor : Sprite2D
{
    public string Key;
    public Vector2 Coordinates { get; set; }
    private List<Action> actions;

    public virtual void Setup()
    {
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
        var action = actions.Find(x => x.InputAction == inputAction);
        return action.Execute(source, world, target);
    }
}
