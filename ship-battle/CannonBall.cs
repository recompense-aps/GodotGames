using Godot;
using HonedGodot;
using HonedGodot.Extensions;

public class CannonBall : RigidBody2D, IZoneListener
{
    [Export]
	public float BaseImpulse { get; private set; } = 500;

	private Vector2 fireDirection = new Vector2(0,0);
	private bool appliedImpulse = false;

	private static readonly PackedScene scene = GD.Load<PackedScene>("res://CannonBall.tscn");

	public static CannonBall InstanceAndFire(Node shooter, Vector2 firePoint, Vector2 fireDirection)
	{
		var ball = scene.Instance<CannonBall>();

		shooter.GetTree().Root.AddChild(ball);
		ball.GlobalPosition = firePoint;
		ball.fireDirection = fireDirection.Normalized();

		return ball;
	}

	public override void _Ready()
	{
		Global.CollisionLayers.SetLayers(this, CollisionLayers.Projectiles);
		Global.CollisionLayers.SetMasks(this, CollisionLayers.Ships, CollisionLayers.Zones);

		this.InlineCallDeffered(() => 
		{
			Global.Log($"created cannon ball at [{GlobalPosition}] owned by {this.GetTag<GameOwner>()?.Data?.Name}", LogTag.ConnonBall);
		});

		this.InlineConnect<Node>(this, Constants.Signal_RigidBody2D_BodyEntered, node => 
		{
			Global.Log($"cannon ball owned by {this.GetTag<GameOwner>()?.Data?.Name} collided with {node.Name}", LogTag.ConnonBall);

			if (node is ICannonBallListener)
				node.As<ICannonBallListener>().OnCollision(this);
		});

	}

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		if (!appliedImpulse)
		{
			ApplyImpulse(Vector2.Zero, fireDirection * BaseImpulse);
			appliedImpulse = true;
		}
	}

	public void Explode()
	{
		Explosion.InstanceAt(this, GlobalPosition, Explosion.ExplosionType.Normal);
		GetTree().Root.AddChild(SceneLoader.Instance<CannonSoundEffect>());

		this.InlineCallDeffered(QueueFree);
	}

	public void OnEnteredZone(Zone zone)
	{
		switch(zone.Type)
		{
			case Zone.ZoneType.Fort:
				Explode();
				break;
		}
	}

	public void OnExitedZone(Zone zone) { }
}

public interface ICannonBallListener
{
	CannonBallCollisionResponse OnCollision(CannonBall ball);
}

public class CannonBallCollisionResponse { }
