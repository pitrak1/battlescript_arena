using Godot;
using System;
using System.Collections;

public partial class InfoPanel : Node
{
	private ArrayList history = new ArrayList();

	public void Navigate(Object obj)
	{
		history.Add(obj);

	}

	public void Return()
	{
		history.RemoveAt(history.Count - 1);
	}

	private void updateDisplay()
	{
		if (history[history.Count - 1].GetType() == typeof(Tile))
		{
			displayTileInformation();
		} 
		else if (history[history.Count - 1].GetType() == typeof(Effect))
		{
			displayEffectInformation();
		} 
		else if (history[history.Count - 1].GetType() == typeof(Ability))
		{
			displayAbilityInformation();
		} 
		else 
		{
			GD.Print("should not get here");
		}
	}

	private void displayTileInformation()
	{

	}

	private void displayEffectInformation()
	{

	}

	private void displayAbilityInformation()
	{

	}
}
