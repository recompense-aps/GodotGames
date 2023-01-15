using Godot;
using System;

public class AutoTest : RigidBody2D
{
    public override void _Ready()
    {
        
    }

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		float x = 0;
		float y = 0;

		if (Input.IsActionPressed(Inputs.key_move_right))
			x = 1;
		if (Input.IsActionPressed(Inputs.key_move_left))
			x = -1;
		if (Input.IsActionPressed(Inputs.key_move_down))
			y = 1;
		if (Input.IsActionPressed(Inputs.key_move_up))
			y = -1;

		var baseVector = new Vector2(x, y);
		var normalizedVelocity = state.LinearVelocity.Normalized();

		var linearForce = 100 * baseVector;

		AppliedForce = linearForce;

		Rotation = normalizedVelocity.Angle() - ( Mathf.Pi / 2 ) ;
	}

}
