using Godot;
using HonedGodot;
using HonedGodot.Extensions;

[LoadScene("res://Explosion.tscn")]
public class Explosion : AnimatedSprite
{
	public static Explosion InstanceAt(Node context, Vector2 globalPosition)
	{
		var explosion = SceneLoader.Instance<Explosion>();
		
		context.GetTree().Root.AddChild(explosion);
		explosion.GlobalPosition = globalPosition;

		return explosion;
	}

    public override void _Ready()
    {
		Play();

        this.InlineConnect(this, "animation_finished", QueueFree);
    }
}
