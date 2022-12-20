using Godot;
using System;

public class Global : Node
{
    public override void _Ready()
    {
        
    }

 	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("exit_game"))
		{
			GetTree().Quit();
		}
	}
}
