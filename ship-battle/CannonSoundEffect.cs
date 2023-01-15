using Godot;
using HonedGodot;
using HonedGodot.Extensions;

[LoadScene("res://CannonSoundEffect.tscn")]
public class CannonSoundEffect : AudioStreamPlayer
{
    public override void _Ready()
    {
		if (!Global.Instance.EnabledSound)
			QueueFree();
		else
			Play();

        this.InlineConnect(this, Constants.Signal_AudioStreamPlayer_Finished, () => 
		{
			Global.Log($"Killed {nameof(CannonSoundEffect)}", LogTag.General);
		});
    }
}
