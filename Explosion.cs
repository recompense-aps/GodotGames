using Godot;
using HonedGodot;
using HonedGodot.Extensions;

[LoadScene("res://Explosion.tscn")]
public class Explosion : AnimatedSprite
{
	public enum ExplosionType { Normal, Smoke, SmokeFire }

	public static Explosion InstanceAt(Node context, Vector2 globalPosition, ExplosionType explosionType = ExplosionType.Normal)
	{
		var explosion = SceneLoader.Instance<Explosion>();
		
		context.GetTree().Root.AddChild(explosion);
		explosion.GlobalPosition = globalPosition;

		switch(explosionType)
		{
			case ExplosionType.Normal:
				explosion.Animation = "default";
				break;
			case ExplosionType.Smoke:
				explosion.Animation = "alt1";
				break;
			case ExplosionType.SmokeFire:
				explosion.Animation = "alt2";
				break;
		}

		return explosion;
	}

    public override void _Ready()
    {
		Play();

        this.InlineConnect(this, "animation_finished", QueueFree);
    }
}
