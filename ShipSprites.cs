using Godot;
using System;

public class ShipSprites : Node2D
{
	private Ship parent;

    public override void _Ready()
    {
        parent = GetParent<Ship>();

		foreach (var child in GetChildren())
			(child as Sprite).Hide();

		GetNode<Sprite>(GetSpriteName()).Show();
    }

	private string GetSpriteName()
	{
		return "default_"
		+ 
		(
			parent.BaseShipType == BaseShipType.Red ? "red" :
			parent.BaseShipType == BaseShipType.Blue ? "blue" :
			parent.BaseShipType == BaseShipType.Green ? "green" :
			parent.BaseShipType == BaseShipType.Yellow ? "yellow" : throw new Exception()
		)
		+
		(
			parent.DamageLevel == ShipDamageLevel.Healthy ? "" :
			parent.DamageLevel == ShipDamageLevel.Damaged ? "_damaged" :
			parent.DamageLevel == ShipDamageLevel.VeryDamaged ? "_very_damaged" : throw new Exception()
		);
	}
}
