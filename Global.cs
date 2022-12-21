using Godot;
using System;

public enum LogTag { General }

public class Global : Node
{
	public static Global Instance { get; private set; }

	private static readonly HonedGodot.Logger<LogTag> logger = new HonedGodot.Logger<LogTag>("game");

	public static void Log(object what, LogTag tag)
	{
		logger.Log(what, tag);
	}

    public override void _Ready()
    {
		Instance = this;
        HonedGodot.ErrorHandling.Configure("res://ErrorScreen.tscn", this);
    }

 	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("exit_game"))
		{
			GetTree().Quit();
		}
	}
}
