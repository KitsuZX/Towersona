using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : TemporalEffect
{
	public float speedBoostAmount;

	private Enemy enemy;
	private Color speedColor = new Color(0.2f, 0.7f, 0.33f, 0.5f);

	public void Initialize(float amount, float time, GameObject target)
	{
		this.target = target;
		this.speedBoostAmount = amount;

		initialTime = time;
		currentTime = time;
		effectType = TemporalEffectType.SpeedBoost;

		enemy = target.GetComponent<Enemy>();
	}

	public override void ApplyEffect()
	{
		if (!enemy.IsAffactedByEffect(effectType))
		{
			//enemy.Tint(speedColor);
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
				SpeedBoost enemySpeedBoost = (SpeedBoost)enemy.GetEffect(effectType);
				if (speedBoostAmount > enemySpeedBoost.speedBoostAmount)
				{
					enemySpeedBoost.speedBoostAmount = speedBoostAmount;
				}
			}
		}
	}

	public override void RemoveEffect()
	{
		enemy.RemoveTemporalEffect(this);
		//enemy.RemoveTint();
		applied = false;
	}
}
