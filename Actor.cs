using Godot;
using System.Collections.Generic;

public enum ActorType
{
    Wolf
}



public partial class Actor : Sprite2D
{
    public string Key;
    public string DisplayName;
    public Vector2 Coordinates { get; set; }
    private List<Action> actions = new List<Action>();

    private ActorType type;

    private int maxHealth;
    private int currentHealth;

    private Dictionary<ActorType, Texture2D> spriteMap = new Dictionary<ActorType, Texture2D> {
        {ActorType.Wolf, GD.Load<Texture2D>("res://assets/wolf.jpeg")},
    };

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        GetNode<Label>("LifeLabel").Text = $"{currentHealth}/{maxHealth}";
    }

    public void AlterHealth(int healthDiff)
    {
        this.currentHealth += healthDiff;
        GetNode<Label>("LifeLabel").Text = $"{currentHealth}/{maxHealth}";
    }

    public virtual void SetType(ActorType type)
    {
        this.type = type;
        this.Texture = spriteMap[type];
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

    public List<Action> GetActions()
    {
        return actions;
    }
}
