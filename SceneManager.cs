using Godot;
using System;

public partial class SceneManager : Node
{
	private Node? currentSceneNode;

	private Dictionary<string, string> nameToSceneMap = new Dictionary<string, string>()
	{
		{"Battle", "res://scenes/battle/BattleManager.tscn"}
	};

	public override void _Ready()
	{
		LoadScene("Battle");
	}

	public void LoadScene(string sceneName)
	{
		if (currentSceneNode is not null)
		{
			RemoveChild(currentSceneNode);
		}

		var sceneNode = GD.Load<PackedScene>(nameToSceneMap[sceneName]).Instantiate();
		AddChild(sceneNode);
		currentSceneNode = sceneNode;
	}
}
