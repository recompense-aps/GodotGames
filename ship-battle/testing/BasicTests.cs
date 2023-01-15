using Godot;
using HonedGodot;

public class BasicTests : Node2D
{
	[GetNode("TextEdit")]
	private TextEdit console { get; set; }

    public override void _Ready()
    {
        HG.GetNodes(this);

		console.Hide();
    }

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed(Inputs.open_console))
		{
			console.Visible = !console.Visible;
		}

		if (Input.IsActionJustPressed(Inputs.execute_console))
		{
			var data = new CommandData(console.Text);
			data.DefaultExecute(this);
		}
	}
}
