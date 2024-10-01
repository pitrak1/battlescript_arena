using Godot;
using System;
using System.Collections.Generic;

public abstract partial class TileEffect : Effect
{
    public Tile tile;

    public TileEffect(
        string key, 
        string displayName, 
        string iconAsset, 
        int duration, 
        Tile t
    ) : base(
        key, 
        displayName, 
        iconAsset, 
        duration)
    {
        tile = t;
    }
}
