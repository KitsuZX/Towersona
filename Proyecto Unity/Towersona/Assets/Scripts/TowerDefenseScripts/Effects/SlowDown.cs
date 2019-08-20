using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : TemporalEffect
{
	public float slowDownAmount;
	public SlowDownType slowDownType;

	private Enemy enemy;
	private Color freezeColor = new Color(0.7f, 0.8f, 0.98f, 0.5f);

	public void Initialize(float amount, float time, SlowDownType slowDownType, GameObject target)
	{
		this.time = time;
		this.target = target;

		this.slowDownAmount = amount;
		this.slowDownType = slowDownType;

		effectType = TemporalEffectType.SlowDown;

		enemy = target.GetComponent<Enemy>();
	}

	public override void ApplyEffect()
	{
		enemy.Tint(freezeColor);
		enemy.AddTemporalEffect(this);
		applied = true;
	}

	public override void RemoveEffect()
	{	
		enemy.RemoveTemporalEffect(this);
		enemy.RemoveTint();
		applied = false;
	}
	
}

public enum SlowDownType { Fox, Area }
