using Godot;
using System.Collections.Generic;
using HonedGodot;
using HonedGodot.Extensions;

public class Zone : Area2D
{
	public enum ZoneType { OutOfBounds, Land, Fort }

	public static List<Zone> All => Global.GetNodesInGroup<Zone>(nameof(Zone));

	[Export]
	public ZoneType Type { get; private set; }

	[Export]
	public bool Debug { get; private set; }

	public override void _Ready()
	{
		Global.CollisionLayers.SetLayers(this, CollisionLayers.Zones);
		
		this.InlineConnect<Node>(this, Constants.Signal_Area2D_BodyEntered, HandleItemEntered);
		this.InlineConnect<Node>(this, Constants.Signal_Area2D_BodyEntered, HandleItemExited);
	}

	private void HandleItemEntered(Node item)
	{
		if (item is IZoneListener)
		{
			if (Debug)
			{
				Global.Log($"Item entered {item.Name}", LogTag.Zone);
			}
			(item as IZoneListener).OnEnteredZone(this);
		}

		if (Type == ZoneType.OutOfBounds)
		{
			item.QueueFree();
		}
	}

	private void HandleItemExited(Node item)
	{
		if (item is IZoneListener)
		{
			if (Debug)
			{
				Global.Log($"Item exited {item.Name}", LogTag.Zone);
			}
			(item as IZoneListener).OnExitedZone(this);
		}
	}
    
}

public interface IZoneListener
{
	void OnEnteredZone(Zone zone);
	void OnExitedZone(Zone zone);
}
