using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Burn : TemporalEffect
{
	public float burnDamage;

	private Enemy enemy;
	private BurnComponent burnComponent;
	private System.Threading.Timer timer;
	private Color burnColor1 = new Color(0.843f, 0.412f, 0f, 1f);
	private Color burnColor2 = new Color(0.502f, 0.035f, 0.035f, 1f);

	public void Initialize(float amount, float time, GameObject target, GameObject source)
	{
		this.burnDamage = amount;

		base.target = target;
		base.source = source;

		initialTime = time;
		currentTime = time;
		effectType = TemporalEffectType.Burn;

		enemy = target.GetComponent<Enemy>();
	}

	public override void ApplyEffect()
	{
		if (!enemy.IsAffactedByEffect(effectType))
		{
			enemy.AddTemporalEffect(this);
			burnComponent = target.AddComponent<BurnComponent>();
			burnComponent.StartBurningEnemy(burnDamage, enemy, burnColor1, burnColor2);

			applied = true;
		}
		else
		{
			//If the enemy is alredy affected by this effect, reset the timer
			enemy.ResetEffectCountdown(effectType);

			//Check if this new effect is stronger than the previous one. If it is, make the alredy existing effect stronger
			Burn enemyBurn = (Burn)enemy.GetEffect(effectType);
			if (burnDamage > enemyBurn.burnDamage)
			{
				enemyBurn.burnDamage = burnDamage;
				burnComponent.burnDamage = burnDamage;
			}

		}
	}

	public override void RemoveEffect()
	{
		enemy.RemoveTemporalEffect(this);
		burnComponent.StopBurningEnemy();

		applied = false;
	}
}
