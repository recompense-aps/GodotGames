using Godot;
using HonedGodot;
using HonedGodot.Extensions;
using System;
using System.Collections.Generic;

public enum BaseShipType { Red, Blue, Green, Yellow }
public enum ShipDamageLevel { Healthy, Damaged, VeryDamaged }
public class Ship : RigidBody2D, ICannonBallListener
{
	[Export]
	private bool Debug = false;

	[Export]
	public BaseShipType BaseShipType { get; private set; }  = BaseShipType.Red;

	[Export]
	public ShipDamageLevel DamageLevel { get; private set; } = ShipDamageLevel.Healthy;

	[Export]
	public float BaseLinearMovementForceModifier { get; private set; } = 100;

	[Export]
	public float BaseShootImpulseModifier { get; private set; } = 10;

	private Vector2 moveVector { get; set; }
	private Func<bool> boostLock { get; set; } = () => true;
	private Vector2 shootPositionRight => GetNode<Node2D>("CannonSpriteRight/ShootPosition").GlobalPosition;
	private Vector2 shootPositionLeft => GetNode<Node2D>("CannonSpriteLeft/ShootPosition").GlobalPosition;
	private Stack<Action> physicsActions = new Stack<Action>();
	
    public override void _Ready()
    {
		HG.GetNodes(this);

		Global.CollisionLayers.SetLayers(this, 
			CollisionLayers.Ships
		);
		Global.CollisionLayers.SetMasks(this, 
			CollisionLayers.Ships,
			CollisionLayers.Land,
			CollisionLayers.Zones
		);

		if (Debug)
		{
			NodeInfo
				.Create(this)
				.AddForCollisionLayerManagement(Global.CollisionLayers)
				.WithPosition(new Vector2(0, -75));
		}
    }

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		var normalizedVelocity = state.LinearVelocity.Normalized();
		var linearForce = BaseLinearMovementForceModifier * moveVector;

		AppliedForce = linearForce;
		Rotation = normalizedVelocity.Angle() - ( Mathf.Pi / 2 );

		while(physicsActions.Count > 0)
			physicsActions.Pop()();
	}

	public void HandleCannonBallCollision(CannonBall ball)
	{
		throw new NotImplementedException();
	}

	public void Move(Vector2 newMoveVector)
	{
		moveVector = newMoveVector;
	}

	public void Shoot(bool right = true)
	{
		var shootPosition = right ? shootPositionRight : shootPositionLeft;
		var shootVector = new Vector2(right ? 1 : -1, 0).Rotated(Rotation);

		var cannonBall = CannonBall.InstanceAndFire(this, shootPosition, shootVector);

		cannonBall.AddCollisionExceptionWith(this);

		this.GetTag<GameOwner>().Propagate(
			cannonBall,
			Explosion.InstanceAt(this, shootPosition, Explosion.ExplosionType.Smoke)
		);

		AddChild(SceneLoader.Instance<CannonSoundEffect>());
	}

	public void Boost()
	{
		// if (!boostLock())
		// 	return;
		
		// boostLock = this.TemporaryEffect(
		// 	() => speedModifier *= 2,
		// 	() => speedModifier /= 2,
		// 	1
		// );
	}

	public CannonBallCollisionResponse OnCollision(CannonBall ball)
	{
		ball.Explode();

		return new CannonBallCollisionResponse();
	}
}
