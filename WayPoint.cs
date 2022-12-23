using Godot;
using HonedGodot;
using System.Collections.Generic;

[LoadScene("res://WayPoint.tscn")]
public class WayPoint : Sprite
{
	public enum WaypointType { Treasure, Base }
	public static List<WayPoint> All => Global.GetNodesInGroup<WayPoint>(nameof(WayPoint));

	[Export]
	public WaypointType Type { get; private set; }

	[Export]
	public bool ShouldShow { get; private set; }

	public override void _Ready()
	{
		Visible = ShouldShow;
	}
    
}
