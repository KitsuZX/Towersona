using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaBullet : Shooting
{
	[HideInInspector]
	public float explosionRadius = 0f;

	protected override void HitTarget()
	{
		DragonStats dragonStats = (DragonStats)stats;

		Vector3 pos = transform.position;
		pos.y += 1f;

		Enemy firstTarget = target.GetComponent<Enemy>();

		if(firstTarget != null)
		{
			firstTarget.TakeDamage(stats.AttackStrength);
		}

		Collider[] colliders = Physics.OverlapSphere(pos, dragonStats.currentDamageArea);

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				Enemy e = collider.GetComponent<Enemy>();
				if (e != null && e != firstTarget)
				{
					e.TakeDamage(stats.AttackStrength * dragonStats.currentAreaDamageReduction);
				}
			}

		}

		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
