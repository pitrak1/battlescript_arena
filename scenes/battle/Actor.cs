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
    public Vector2 Coordinates { get; set; }
    public int Speed;
    private List<Ability> abilities = new List<Ability>();
    public List<Effect> Effects = new List<Effect>();
    private int maxHealth;
    private int currentHealth;
    private ActorTypes type;

    public void Setup(ActorTypes actorType, Vector2 coordinates)
    {
        this.type = actorType;
        this.Coordinates = coordinates;

        if (actorType == ActorTypes.Wolf)
        {
            this.Texture = GD.Load<Texture2D>("res://assets/wolf.jpeg");
            this.Speed = 15;
            this.DisplayName = "Wolf";

            AddAbility(new MoveAbility("Q"));
            AddAbility(new HurtSelfAbility("W"));
            AddAbility(new InvokeEarthAbility("E"));
            SetMaxHealth(15);
        }
        else if (actorType == ActorTypes.Turtle)
        {
            this.Texture = GD.Load<Texture2D>("res://assets/turtle.jpeg");
            this.Speed = 20;
            this.DisplayName = "Turtle";

            AddAbility(new MoveAbility("Q"));
            AddAbility(new HurtSelfAbility("W"));
            AddAbility(new ThrowDirtAbility("E"));
            SetMaxHealth(20);
        }
    }

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

    public void Place(Vector2 coords)
    {
        this.Coordinates = coords;
    }

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public void EndTurn(Actor actor)
    {
        abilities.ForEach(ability =>
        {
            ability.EndTurn();
        });

        Effects.ForEach(effect =>
        {
            effect.EndTurn();
        });
    }

    public bool ExecuteAbility(string inputAction, Actor source, World world, List<Vector2> target, Spectrum spectrum)
    {
        var ability = abilities.Find(x => x.InputAction == inputAction);
        return ability.Execute(source, world, target, spectrum);
    }

    public List<Ability> GetAbilities()
    {
        return abilities;
    }
}
