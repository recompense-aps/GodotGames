using Godot;
using HonedGodot;
using HonedGodot.Extensions;
using System;

public enum BaseShipType { Red, Blue, Green, Yellow }
public enum ShipDamageLevel { Healthy, Damaged, VeryDamaged }
public class Ship : KinematicBody2D
{
	[Export]
	private bool Debug = false;

	[Export]
	public BaseShipType BaseShipType = BaseShipType.Red;

	[Export]
	public ShipDamageLevel DamageLevel = ShipDamageLevel.Healthy;

	[Export]
	private float BaseSpeed = 100;

	[Export]
	private float BaseAcceleration = 1;

	[Export]
	private float MaxMovementSpeed = 200;

	[Export]
	private float BoostFactor = 2;

	private Vector2 baseMovementDirection { get; set; }
	private Vector2 rightShootPosition => GetNode<Node2D>("RightShootPoint").Position;
	private Vector2 leftShootPosition => GetNode<Node2D>("LeftShootPoint").Position;
	private float ComputedMovementSpeed => Mathf.Clamp(BaseSpeed + movingSpeedModifier, 0, MaxMovementSpeed);
	private Vector2 velocity => baseMovementDirection * ComputedMovementSpeed * speedModifier;
	private float speedModifier { get; set; } = 1;
	private float movingSpeedModifier = 0;
	private Func<bool> boostLock { get; set; } = () => true;
	
    public override void _Ready()
    {
		if (Debug)
		{
			NodeInfo.Add(this,
				() => $"v:  {velocity}",
				() => $"ms: {movingSpeedModifier}",
				() => $"sm: {speedModifier}"
			).WithPosition(new Vector2(0, -125));
		}
    }

	public override void _Process(float delta)
	{
		movingSpeedModifier = Mathf.Clamp(movingSpeedModifier, 0, 50);
	}

	public override void _PhysicsProcess(float delta)
	{
		MoveAndSlide(velocity);
	}

	public void HandleCannonBallCollision(CannonBall ball)
	{
		throw new NotImplementedException();
	}

	public void MoveTowards(Vector2 vector)
	{
		
		movingSpeedModifier += BaseAcceleration;
		baseMovementDirection = vector.Normalized();
		Rotation = baseMovementDirection.Length() > 0 ?
			vector.Angle() - ( Mathf.Pi / 2 ) :
			Rotation;
	}

	public void MoveStop()
	{
		if (movingSpeedModifier > 0)
		{
			movingSpeedModifier -= BaseAcceleration;
		}
		else
		{
			movingSpeedModifier = 0;
			baseMovementDirection = Vector2.Zero;
		}
	}

	public void Shoot(bool right)
	{
		if (right)
		{
			CannonBall.InstanceAndFire(this, Position + rightShootPosition, new Vector2(1, 0).Rotated(Rotation));
			Explosion.InstanceAt(this, rightShootPosition);
		}
		else
		{
			CannonBall.InstanceAndFire(this, Position + leftShootPosition, new Vector2(-1, 0).Rotated(Rotation));
			Explosion.InstanceAt(this, leftShootPosition);
		}

		baseMovementDirection += (-1 * baseMovementDirection / 2);
	}

	public void Boost()
	{
		if (!boostLock())
			return;
		
		boostLock = this.TemporaryEffect(
			() => speedModifier *= 2,
			() => speedModifier /= 2,
			1
		);
	}
}
