using System;

public class GameOwner : Godot.Object
{
	public static readonly GameOwner Computer = new GameOwner("computer");
	public static readonly GameOwner Player1 = new GameOwner("player1");
	public static readonly GameOwner Player2 = new GameOwner("player2");
	public static readonly GameOwner[] All = new GameOwner[]{ Computer, Player1, Player2 };

	public string Name { get; set; }
	public Guid Id { get; private set; }
	public bool Active { get; private set; }

	public GameOwner(string name)
	{
		Name = name;
		Id = Guid.NewGuid();
	}
}