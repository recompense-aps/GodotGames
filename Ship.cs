using Godot;
using System;

public enum BaseShipType { Red, Blue, Green, Yellow }
public enum ShipDamageLevel { Healthy, Damaged, VeryDamaged }
public class Ship : KinematicBody2D
{
	[Export]
	public BaseShipType BaseShipType = BaseShipType.Red;

	[Export]
	public ShipDamageLevel DamageLevel = ShipDamageLevel.Healthy;

	[Export]
	private float BaseSpeed = 100;

	private Vector2 baseMovementDirection { get; set; }
	private Vector2 rightShootPosition => GetNode<Node2D>("RightShootPoint").Position;
	private Vector2 leftShootPosition => GetNode<Node2D>("LeftShootPoint").Position;
	
    public override void _Ready()
    {
    }

	public override void _Process(float delta)
	{
		
	}

	public override void _PhysicsProcess(float delta)
	{
		MoveAndSlide(baseMovementDirection * BaseSpeed);
	}

	public void HandleCannonBallCollision(CannonBall ball)
	{
		// if (ball.Shooter.)
	}

	public void MoveTowards(Vector2 vector)
	{
		baseMovementDirection = vector.Normalized();
		Rotation = baseMovementDirection.Length() > 0 ?
			vector.Angle() - ( Mathf.Pi / 2 ) :
			Rotation;
	}

	public void Shoot(bool right)
	{
		if (right)
			CannonBall.InstanceAndFire(this, Position + rightShootPosition, new Vector2(1, 0).Rotated(Rotation));
		else
			CannonBall.InstanceAndFire(this, Position + leftShootPosition, new Vector2(-1, 0).Rotated(Rotation));
	}
}
