using Godot;
using HonedGodot;

public class BasicTests : Node2D
{
    public override void _Ready()
    {
        HG.GetNodes(this);
    }
}
