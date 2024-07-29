using Godot;
using System.Collections.Generic;

public enum Elements
{
    Wind,
    Earth,
    Fire,
    Water
}

public partial class ElementalSpectra : Control
{
    List<TextureRect> windEarthGauge;
    int currentWindEarthValue;
    List<TextureRect> fireWaterGauge;
    int currentFireWaterValue;

    public override void _Ready()
    {
        windEarthGauge = new List<TextureRect>() {
            GetNode<TextureRect>("WindHighlight"),
            GetNode<TextureRect>("WindTick2"),
            GetNode<TextureRect>("WindTick1"),
            GetNode<TextureRect>("WindEarthNeutralTick"),
            GetNode<TextureRect>("EarthTick1"),
            GetNode<TextureRect>("EarthTick2"),
            GetNode<TextureRect>("EarthHighlight"),
        };

        fireWaterGauge = new List<TextureRect>() {
            GetNode<TextureRect>("FireHighlight"),
            GetNode<TextureRect>("FireTick2"),
            GetNode<TextureRect>("FireTick1"),
            GetNode<TextureRect>("FireWaterNeutralTick"),
            GetNode<TextureRect>("WaterTick1"),
            GetNode<TextureRect>("WaterTick2"),
            GetNode<TextureRect>("WaterHighlight"),
        };

        Reset();
    }

    public void Reset()
    {
        currentWindEarthValue = 3;
        currentFireWaterValue = 3;
        updateSprites();
    }

    public void IncreaseElement(Elements element, int diff = 1)
    {
        switch (element)
        {
            case Elements.Wind:
                currentWindEarthValue = Mathf.Max(currentWindEarthValue - diff, 0);
                break;
            case Elements.Earth:
                currentWindEarthValue = Mathf.Min(currentWindEarthValue + diff, 6);
                break;
            case Elements.Fire:
                currentFireWaterValue = Mathf.Max(currentFireWaterValue - diff, 0);
                break;
            case Elements.Water:
                currentFireWaterValue = Mathf.Min(currentFireWaterValue + diff, 6);
                break;
        }
        updateSprites();
    }

    public int GetElementPower(Elements element)
    {
        switch (element)
        {
            case Elements.Wind:
                return 3 - currentWindEarthValue;
            case Elements.Earth:
                return currentWindEarthValue - 3;
            case Elements.Fire:
                return 3 - currentFireWaterValue;
            case Elements.Water:
                return currentFireWaterValue - 3;
            default:
                return -4;
        }
    }

    private void updateSprites()
    {
        for (int i = 0; i < 7; i++)
        {
            if (i == currentWindEarthValue)
            {
                windEarthGauge[i].Visible = true;
            }
            else
            {
                windEarthGauge[i].Visible = false;
            }
        }

        for (int i = 0; i < 7; i++)
        {
            if (i == currentFireWaterValue)
            {
                fireWaterGauge[i].Visible = true;
            }
            else
            {
                fireWaterGauge[i].Visible = false;
            }
        }
    }
}
