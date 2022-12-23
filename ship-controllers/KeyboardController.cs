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
		float x=0, y=0;

		if (Input.IsActionPressed(Inputs.key_move_right))
			x = 1;

		if (Input.IsActionPressed(Inputs.key_move_left))
			x = -1;

		if (Input.IsActionPressed(Inputs.key_move_up))
			y = -1;

		if (Input.IsActionPressed(Inputs.key_move_down))
			y = 1;

		ship.Move(new Vector2(x, y));

		if (Input.IsActionJustPressed(Inputs.boost_ship))
			ship.Boost();
	}
}
