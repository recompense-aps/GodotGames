using Godot;
using HonedGodot;
using System.Collections.Generic;
using System.Linq;

public enum LogTag { General, Zone, ConnonBall }
public enum CollisionLayers { Land, Ships, Zones, Crewman, Projectiles}

public class Global : Node
{
	public static Global Instance { get; private set; }

	[Export]
	public LogTag[] DisabledTags { get; private set; }

	[Export]
	public bool EnabledSound { get; private set; } = false;

	public static readonly CollisionLayerManager<CollisionLayers> CollisionLayers = new CollisionLayerManager<CollisionLayers>();

	private static readonly HonedGodot.Logger<LogTag> logger = new HonedGodot.Logger<LogTag>("game");

	public static void Log(object what, LogTag tag)
	{
		logger.Log(what, tag);
	}

	public static List<T> GetNodesInGroup<T>(string group) where T:Node
	{
		return Instance.GetTree().GetNodesInGroup(group).Cast<T>().ToList();
	}

    public override void _Ready()
    {
		Instance = this;
        HonedGodot.ErrorHandling.Configure("res://ErrorScreen.tscn", this);
		HonedGodot.SceneLoader.Load();
		HonedGodot.HG.GenerateInputMapConstants();
		ConfigureActiveLogs();
    }

 	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed(Inputs.exit_game ))
		{
			GetTree().Quit();
		}
	}

	private void ConfigureActiveLogs()
	{
		if (DisabledTags == null)
			return;

		foreach(var item in DisabledTags)
		{
			logger.Disable(item);
		}
	}
}
