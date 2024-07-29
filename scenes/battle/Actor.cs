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
    private List<Ability> abilities = new List<Ability>();
    public List<Effect> Effects = new List<Effect>();
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

                abilities = new List<Ability>();
                abilities.Add(new MoveAbility("Q"));
                abilities.Add(new HurtSelfAbility("W"));
                abilities.Add(new InvokeEarthAbility("E"));

                MaxHealth = 15;
            }
            else if (value == ActorTypes.Turtle)
            {
                this.Texture = GD.Load<Texture2D>("res://assets/turtle.jpeg");
                this.Speed = 20;
                this.DisplayName = "Turtle";

                abilities = new List<Ability>();
                abilities.Add(new MoveAbility("Q"));
                abilities.Add(new HurtSelfAbility("W"));
                abilities.Add(new ThrowDirtAbility("E"));

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

    // public void Place(Vector2 coords)
    // {
    //     this.Coordinates = coords;
    // }

    // public void AddAbility(Ability ability)
    // {
    //     abilities.Add(ability);
    // }

    // public void EndTurn(Actor actor)
    // {
    //     abilities.ForEach(ability =>
    //     {
    //         ability.EndTurn();
    //     });

    //     Effects.ForEach(effect =>
    //     {
    //         effect.EndTurn();
    //     });
    // }

    // public bool ExecuteAbility(string inputAction, Actor source, World world, List<Vector2> target, Spectrum spectrum)
    // {
    //     var ability = abilities.Find(x => x.InputAction == inputAction);
    //     return ability.Execute(source, world, target, spectrum);
    // }

    // public List<Ability> GetAbilities()
    // {
    //     return abilities;
    // }
}
