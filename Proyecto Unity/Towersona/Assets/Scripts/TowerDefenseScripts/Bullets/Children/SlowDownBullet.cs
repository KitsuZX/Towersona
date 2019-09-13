using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBullet : Shooting
{
	FoxAttack foxAttack;

	private void Start()
	{
		this.foxAttack = (FoxAttack)base.pattern;
	}

	protected override void HitTarget()
	{
		Enemy e = target.parent.GetComponent<Enemy>();

		Vector3 pos = transform.position;

		pos.y += 1f;

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		if (e != null)
		{
			e.TakeDamage(foxAttack.AttackStrength);
			
			SlowDown slowDown = (SlowDown)TemporalEffect.CreateEffect(TemporalEffectType.SlowDown);
			slowDown.Initialize(foxAttack.currentSlowDownPercentage, foxAttack.currentSlowDownTime, target.gameObject);
			slowDown.ApplyEffect();
			
		}

		Destroy(gameObject);
	}
}
