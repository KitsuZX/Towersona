using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : TemporalEffect
{
	public float slowDownAmount;	

	private Enemy enemy;
	private Color freezeColor = new Color(0.7f, 0.8f, 0.98f, 0.5f);

	public void Initialize(float amount, float time, GameObject target)
	{
		this.target = target;
		this.slowDownAmount = amount;	
		
		initialTime = time;
		currentTime = time;
		effectType = TemporalEffectType.SlowDown;

		enemy = target.GetComponentInParent<Enemy>();
	}

	public override void ApplyEffect()
	{
		if (!enemy.IsAffactedByEffect(effectType))
		{
			enemy.Tint(freezeColor);
			enemy.AddTemporalEffect(this);
			applied = true;
		}
		else
		{
			//Check if it's a permanent slowdown (lasers)
			if (initialTime != Mathf.Infinity)
			{
				//If the enemy is alredy affected by this effect, reset the timer
				enemy.ResetEffectCountdown(effectType);

				//Check if this new effect is stronger than the previous one. If it is, make the alredy existing effect stronger
				SlowDown enemySlowDown = (SlowDown)enemy.GetEffect(effectType);
				if (slowDownAmount > enemySlowDown.slowDownAmount)
				{
					enemySlowDown.slowDownAmount = slowDownAmount;
				}
			}

		}
	}

	public override void RemoveEffect()
	{	
		enemy.RemoveTemporalEffect(this);
		enemy.RemoveTint();
		applied = false;
	}	
}
