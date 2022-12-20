using Godot;
using HonedGodot;

public class BasicTests : Node2D
{
   	[GetNode("TestPoint")]
	private Node2D testPoint { get; set; }

    public override void _Ready()
    {
        HG.GetNodes(this);

		float x = testPoint.Position.x;
    }
}
