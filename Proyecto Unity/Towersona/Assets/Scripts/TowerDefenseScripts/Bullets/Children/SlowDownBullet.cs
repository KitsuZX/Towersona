using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBullet : Shooting
{
	FoxStats foxStats;

	private void Start()
	{
		foxStats = (FoxStats)stats;
	}


	protected override void HitTarget()
	{
		Enemy e = target.GetComponent<Enemy>();

		Vector3 pos = transform.position;

		pos.y += 1f;

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		if (e != null)
		{
			e.TakeDamage(stats.AttackStrength);
			if (!e.IsAffactedByEffect(TemporalEffectType.SlowDown))
			{
				SlowDown slowDown = (SlowDown)TemporalEffect.CreateEffect(TemporalEffectType.SlowDown);
				slowDown.Initialize(foxStats.currentSlowDownPercentage, foxStats.currentSlowDownTime, SlowDownType.Fox, target.gameObject);
				slowDown.ApplyEffect();
			}
		}

		Destroy(gameObject);
	}
}
