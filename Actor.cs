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
    private List<Ability> abilities = new List<Ability>();

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

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }

    public bool ExecuteAbility(string inputAction, Actor source, World world, List<Vector2> target)
    {
        var ability = abilities.Find(x => x.InputAction == inputAction);
        return ability.Execute(source, world, target);
    }

    public List<Ability> GetAbilities()
    {
        return abilities;
    }
}
