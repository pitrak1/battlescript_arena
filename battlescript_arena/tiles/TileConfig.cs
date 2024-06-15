using Godot;
using System.Collections.Generic;

public enum TileTypes
{
    High,
    Middle,
    Low,
    Water,
    Rock,
    Tree,
    None
}

public partial class TileConfig
{
    public static Dictionary<TileTypes, PackedScene> TileSceneMap = new Dictionary<TileTypes, PackedScene>() {
        {TileTypes.High, GD.Load<PackedScene>("res://tiles/HighTile.tscn")},
        {TileTypes.Middle, GD.Load<PackedScene>("res://tiles/MiddleTile.tscn")},
        {TileTypes.Low, GD.Load<PackedScene>("res://tiles/LowTile.tscn")},
        {TileTypes.Water, GD.Load<PackedScene>("res://tiles/WaterTile.tscn")},
        {TileTypes.Rock, GD.Load<PackedScene>("res://tiles/RockTile.tscn")},
        {TileTypes.Tree, GD.Load<PackedScene>("res://tiles/TreeTile.tscn")},
        {TileTypes.None, GD.Load<PackedScene>("res://tiles/NoneTile.tscn")},
    };
}
