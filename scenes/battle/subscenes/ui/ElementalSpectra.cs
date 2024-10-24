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
    List<TextureRect> windEarthSpectrum;
    int windEarthValue;

    public int WindPower
    {
        get { return -windEarthValue; }
        set
        {
            windEarthValue = Math.Clamp(-value, -3, 3);
            updateSprites();
        }
    }

    public int EarthPower
    {
        get { return windEarthValue; }
        set
        {
            windEarthValue = Math.Clamp(value, -3, 3);
            updateSprites();
        }
    }
    List<TextureRect> fireWaterSpectrum;
    int fireWaterValue;

    public int FirePower
    {
        get { return -fireWaterValue; }
        set
        {
            fireWaterValue = Math.Clamp(-value, -3, 3);
            updateSprites();
        }
    }

    public int WaterPower
    {
        get { return fireWaterValue; }
        set
        {
            fireWaterValue = Math.Clamp(value, -3, 3);
            updateSprites();
        }
    }

    public override void _Ready()
    {
        windEarthSpectrum = new List<TextureRect>() {
            GetNode<TextureRect>("WindHighlight"),
            GetNode<TextureRect>("WindTick2"),
            GetNode<TextureRect>("WindTick1"),
            GetNode<TextureRect>("WindEarthNeutralTick"),
            GetNode<TextureRect>("EarthTick1"),
            GetNode<TextureRect>("EarthTick2"),
            GetNode<TextureRect>("EarthHighlight"),
        };

        fireWaterSpectrum = new List<TextureRect>() {
            GetNode<TextureRect>("FireHighlight"),
            GetNode<TextureRect>("FireTick2"),
            GetNode<TextureRect>("FireTick1"),
            GetNode<TextureRect>("FireWaterNeutralTick"),
            GetNode<TextureRect>("WaterTick1"),
            GetNode<TextureRect>("WaterTick2"),
            GetNode<TextureRect>("WaterHighlight"),
        };

        windEarthValue = 0;
        fireWaterValue = 0;
        updateSprites();
    }

    private void updateSprites()
    {
        for (int i = -3; i <= 3; i++)
        {
            windEarthSpectrum[i + 3].Visible = i == windEarthValue;
        }

        for (int i = -3; i <= 3; i++)
        {
            fireWaterSpectrum[i + 3].Visible = i == fireWaterValue;
        }
    }
}
