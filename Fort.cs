using System;
using Godot;
using HonedGodot;
using HonedGodot.Extensions;

public class Fort : Node2D
{
	private Vector2 topShootPoint => GetNode<Node2D>("TopShootPoint").GlobalPosition;
	private Vector2 bottomShootPoint => GetNode<Node2D>("BottomShootPoint").GlobalPosition;
	private Vector2 leftShootPoint => GetNode<Node2D>("LeftShootPoint").GlobalPosition;
	private Vector2 rightShootPoint => GetNode<Node2D>("RightShootPoint").GlobalPosition;
	public override void _Ready() 
	{
		// HG.CreateInterval(this, 1, () => 
		// {
		// 	Shoot();
		// });
	}

	public override void _Process(float delta) { }

	private void Shoot()
	{
		Explosion.InstanceAt(this, topShootPoint, Explosion.ExplosionType.Smoke);
		Explosion.InstanceAt(this, bottomShootPoint, Explosion.ExplosionType.Smoke);
		Explosion.InstanceAt(this, leftShootPoint, Explosion.ExplosionType.Smoke);
		Explosion.InstanceAt(this, rightShootPoint, Explosion.ExplosionType.Smoke);

		this.GetTag<GameOwner>().Propagate(
			CannonBall.InstanceAndFire(this, topShootPoint, Vector2.Up),
			CannonBall.InstanceAndFire(this, bottomShootPoint, Vector2.Down),
			CannonBall.InstanceAndFire(this, leftShootPoint, Vector2.Left),
			CannonBall.InstanceAndFire(this, rightShootPoint, Vector2.Right)
		);
	}
}