using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBullet : Shooting
{
	FoxStats foxStats;

	private void Awake()
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
			if (!e.AlredySlownDownByTowersona(source))
			{
			
				e.AddSlowDown(foxStats.currentSlowDownPercentage, foxStats.currentSlowDownTime, Enemy.SlowDownType.Fox, source);
			}
		}

		Destroy(gameObject);
	}
}
