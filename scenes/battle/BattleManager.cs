using Godot;
using System;

public partial class BattleManager : Node2D
{
	private World world;

	private BattleInterface battleInterface;

	public override void _Ready()
	{
		world = GetNode<World>("World");
		battleInterface = GetNode<BattleInterface>("BattleInterface");
	}

	public void _OnTileClicked(Vector2 coordinates)
	{
		world.HighlightTile(coordinates);
	}
}
