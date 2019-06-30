using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBullet : Shooting
{
	[HideInInspector]
	public float slowDownAmount = 0f;
	[HideInInspector]
	public float slowDownTime = 0f;


	protected override void HitTarget()
	{
		Enemy e = target.GetComponent<Enemy>();

		Vector3 pos = transform.position;

		pos.y += 1f;

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		if (e != null)
		{
			e.TakeDamage(damage);
			e.AddSlowDown(slowDownAmount, slowDownTime, Enemy.SlowDownType.Fox);
		}

		Destroy(gameObject);
	}
}
