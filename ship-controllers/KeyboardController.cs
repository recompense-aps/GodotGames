using Godot;
using System;

public class KeyboardController : Node
{
    private Ship ship;

    public override void _Ready()
    {
        ship = GetParent() as Ship;
    }

 	public override void _Process(float delta)
	{
		var movementVector = Input.GetVector(
			Inputs.key_move_left,
			Inputs.key_move_right,
			Inputs.key_move_up,
			Inputs.key_move_down
		);

		ship.Move(movementVector);

		if (Input.IsActionJustPressed(Inputs.mouse_shoot_right))
		{
			ship.Shoot(true);
		}

		if (Input.IsActionJustPressed(Inputs.mouse_shoot_left))
		{
			ship.Shoot(false);
		}
	}
}
