using Godot;
using HonedGodot;

public class ErrorScreen : Node2D
{
    public override void _Ready()
    {
		Global.GetNodesInGroup<Node>(nameof(NodeInfo)).ForEach(x => x.QueueFree());

        GetNode<TextEdit>("TextEdit").Text = HonedGodot.ErrorHandling.CrashError.Message + "\n" + HonedGodot.ErrorHandling.CrashError.StackTrace;
    }
}
