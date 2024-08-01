using Godot;
using System.Collections.Generic;

public enum ActorTypes
{
    Wolf,
    Turtle
}

public partial class Actor : Sprite2D
{
    public string Key;
    public string DisplayName;
    public Vector2 Coordinates;
    public int Speed;
    public List<Ability> Abilities { get; private set; } = new List<Ability>();
    public List<Effect> Effects { get; private set; } = new List<Effect>();
    private int maxHealth;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            currentHealth = value;
            GetNode<Label>("LifeLabel").Text = $"{currentHealth}/{maxHealth}";
        }
    }
    private int currentHealth;
    private ActorTypes type;
    public ActorTypes Type
    {
        get { return type; }
        set
        {
            if (value == ActorTypes.Wolf)
            {
                this.Texture = GD.Load<Texture2D>("res://assets/wolf.jpeg");
                this.Speed = 15;
                this.DisplayName = "Wolf";

                Abilities = new List<Ability>();
                Abilities.Add(new MoveAbility("Q"));
                Abilities.Add(new HurtSelfAbility("W"));
                Abilities.Add(new InvokeEarthAbility("E"));

                MaxHealth = 15;
            }
            else if (value == ActorTypes.Turtle)
            {
                this.Texture = GD.Load<Texture2D>("res://assets/turtle.jpeg");
                this.Speed = 20;
                this.DisplayName = "Turtle";

                Abilities = new List<Ability>();
                Abilities.Add(new MoveAbility("Q"));
                Abilities.Add(new HurtSelfAbility("W"));
                Abilities.Add(new ThrowDirtAbility("E"));

                MaxHealth = 20;
            }
        }
    }

    public void Setup(ActorTypes actorType, Vector2 coordinates)
    {
        Type = actorType;
        Coordinates = coordinates;
    }

    public void AlterHealth(int healthDiff)
    {
        currentHealth += healthDiff;
        GetNode<Label>("LifeLabel").Text = $"{currentHealth}/{maxHealth}";
    }
}
