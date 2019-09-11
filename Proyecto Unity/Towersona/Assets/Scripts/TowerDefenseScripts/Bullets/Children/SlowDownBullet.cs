using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBullet : Shooting
{
	new FoxAttack pattern;

	private void Start()
	{
		this.pattern = (FoxAttack)base.pattern;
	}

	protected override void HitTarget()
	{
		Enemy e = target.GetComponent<Enemy>();

		Vector3 pos = transform.position;

		pos.y += 1f;

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		if (e != null)
		{
			e.TakeDamage(pattern.AttackStrength);
			
			SlowDown slowDown = (SlowDown)TemporalEffect.CreateEffect(TemporalEffectType.SlowDown);
			slowDown.Initialize(pattern.currentSlowDownPercentage, pattern.currentSlowDownTime, target.gameObject);
			slowDown.ApplyEffect();
			
		}

		Destroy(gameObject);
	}
}
