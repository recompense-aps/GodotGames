using System;
using Godot;
using HonedGodot;
using HonedGodot.Extensions;

public class Tagger : Node
{
	[Export]
	public OwnerEnum OwnerType { get; private set; } = OwnerEnum.Computer;

	public override void _Ready() 
	{ 
		var owner = OwnerType == OwnerEnum.Player1 ? GameOwner.Player1 :
					OwnerType == OwnerEnum.Player2 ? GameOwner.Player2 :
					OwnerType == OwnerEnum.Computer ? GameOwner.Computer :
					throw new Exception();

		this.InlineCallDeffered(() => 
		{
			GetParent<Node>().Tag<GameOwner>(owner);
			QueueFree();
		});
	} 
}