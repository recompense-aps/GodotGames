using Godot;
using System;

public class InGameMusic : AudioStreamPlayer
{
    public override void _Ready()
    {
        if (!Global.Instance.EnabledSound)
			QueueFree();
    }
}
