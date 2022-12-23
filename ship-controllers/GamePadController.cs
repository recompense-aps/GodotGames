using Godot;
using System;

public class GamePadController : Node
{
	[Export]
	public int DeviceIndex { get; private set; } = 0;
    private Ship ship;

    public override void _Ready()
    {
        ship = GetParent() as Ship;
    }

	public override void _Process(float delta)
	{
		var movementVector = Input.GetVector(
			$"joystick_left_left_{DeviceIndex}",
			$"joystick_left_right_{DeviceIndex}",
			$"joystick_left_up_{DeviceIndex}",
			$"joystick_left_down_{DeviceIndex}"
		);

		var cannonVector = Input.GetVector(
			$"joystick_right_left_{DeviceIndex}",
			$"joystick_right_right_{DeviceIndex}",
			$"joystick_right_up_{DeviceIndex}",
			$"joystick_right_down_{DeviceIndex}"
		);

		ship.Move(movementVector);

		if (Input.IsActionJustPressed($"shoot_pad_right_{DeviceIndex}"))
		{
			ship.Shoot(true);
		}

		if (Input.IsActionJustPressed($"shoot_pad_left_{DeviceIndex}"))
		{
			ship.Shoot(false);
		}
	}
}
