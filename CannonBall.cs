using Godot;
using System;

public class CannonBall : Area2D
{
    [Export]
	public float BaseSpeed = 3000;

	public Ship Shooter { get; private set; }

	private Vector2 fireDirection = new Vector2(0,0);

	private static readonly PackedScene scene = GD.Load<PackedScene>("res://CannonBall.tscn");

	public static CannonBall InstanceAndFire(Ship shooter, Vector2 firePoint, Vector2 fireDirection)
	{
		var ball = scene.Instance<CannonBall>();

		shooter.GetTree().Root.AddChild(ball);
		ball.GlobalPosition = firePoint;
		ball.fireDirection = fireDirection.Normalized();

		return ball;
	}

	public override void _Process(float delta)
	{
		GlobalPosition += (delta * fireDirection * BaseSpeed);
	}
}
