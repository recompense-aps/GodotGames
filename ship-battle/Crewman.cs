using Godot;
using HonedGodot;
using System.Linq;


[LoadScene("res://Crewman.tscn")]
public class Crewman : KinematicBody2D, IZoneListener
{
	public enum CrewmanState { Idle, Boarded, UnBoarded, Sailing, GetGold, ReturningToShip }

	[Export]
	public bool Debug { get; private set; } = false;

	[Export]
	public float BaseSpeed { get; private set; } = 150;

	[Export]
	public CrewmanState InitialState { get; set; } = CrewmanState.Sailing;

	public FiniteStateMachine<CrewmanState> State { get; private set; }

	public bool IsInLandZone { get; private set; } = false;

	[GetNode("BoatSprite")]
    private Sprite boatSprite { get; set; }

	[GetNode("PersonSprite")]
	private Sprite personSprite { get; set; }

	private WayPoint currentWaypoint = null;
	private Vector2 currentMoveVector = Vector2.Zero;

    public override void _Ready()
    {
        HG.GetNodes(this);
		State = ComposeNormalStates();
		State.Switch(InitialState);

		if (Debug)
		{
			NodeInfo
				.Create(this)
				.AddForStateMachine(State)
				.AddForCollisionLayerManagement(Global.CollisionLayers)
				.WithGetters(
					() => $"mv: {currentMoveVector}",
					() => $"lz: {IsInLandZone}"
				)
				.WithPosition(new Vector2(0, -100));
		}
    }

	public override void _PhysicsProcess(float delta)
	{
		State.Execute();
	}

	private FiniteStateMachine<CrewmanState> ComposeNormalStates()
	{
		var machine = new FiniteStateMachine<CrewmanState>();

		machine.Compose(CrewmanState.Idle);

		machine.Compose(CrewmanState.Boarded)
		.WithStart(m => 
		{
			Hide();
		});

		machine.Compose(CrewmanState.UnBoarded)
		.WithStart(m => 
		{
			boatSprite.Show();
			personSprite.Show();
		});

		machine.Compose(CrewmanState.Sailing)
		.WithStart(m => 
		{
			// TODO: this should find the nearest gold that doesn't belon to controller
			// TODO: path finding
			currentWaypoint = WayPoint.All.First(x => x.Type == WayPoint.WaypointType.Treasure);

			currentMoveVector = GlobalPosition.DirectionTo(currentWaypoint.GlobalPosition);
		})
		.WithExecute(m => 
		{
			MoveAndSlide(currentMoveVector * BaseSpeed);

			if (IsInLandZone)
				State.Switch(CrewmanState.Idle);
		});

		machine.Compose(CrewmanState.GetGold)
		.WithStart(m => 
		{
		});


		return machine;
	}

	public void OnEnteredZone(Zone zone)
	{
		if (zone.Type == Zone.ZoneType.Land)
		{
			IsInLandZone = true;
		}
	}

	public void OnExitedZone(Zone zone)
	{
		if (zone.Type == Zone.ZoneType.Land)
		{
			IsInLandZone = true;
		}
	}
}
