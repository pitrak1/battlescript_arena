using Godot;
using System;

public class ActorTeam
{
	public Color TeamColor { get; private set; } = Colors.White;
	public ActorTeam(Color teamColor)
	{
		TeamColor = teamColor;
	}
}
