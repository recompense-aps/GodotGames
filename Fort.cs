using System;
using Godot;
using HonedGodot;
using HonedGodot.Extensions;

public class Fort : Node2D
{
	public override void _Ready() 
	{
		// var land = GetNode<TileMap>("LandLayer");

		// Global.CollisionLayers.SetLayers(land.CollisionLayer, 
		// 	CollisionLayers.Land
		// );

		// Global.CollisionLayers.SetMasks(land, 
		// 	CollisionLayers.Ships,
		// 	CollisionLayers.Land,
		// 	CollisionLayers.Zones
		// );
	}

	public override void _Process(float delta) { } 
}