using Godot;
using HonedGodot.Extensions;

public class Explosion : AnimatedSprite
{
	private static PackedScene scene = GD.Load<PackedScene>("res://Explosion.tscn");
	public static Explosion InstanceAt(Node context, Vector2 globalPosition)
	{
		var explosion = scene.Instance<Explosion>();
		explosion.GlobalPosition = globalPosition;
		context.AddChild(explosion);

		return explosion;
	}

    public override void _Ready()
    {
		Play();

        this.InlineConnect(this, "animation_finished", () => 
		{
			QueueFree();
		});
    }
}
