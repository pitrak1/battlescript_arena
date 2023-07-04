using Godot;
using System.Collections.Generic;

public partial class Actor : Sprite2D
{
    public string Key;
    public string DisplayName;
    public Vector2 Coordinates { get; set; }
    public int Speed;
    private List<Ability> abilities = new List<Ability>();
    private int maxHealth;
    private int currentHealth;

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

    public void EndTurn()
    {
        abilities.ForEach(ability =>
        {
            ability.EndTurn();
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
