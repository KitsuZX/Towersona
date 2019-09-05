using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownAreaBullet : Shooting
{
	[SerializeField] private GameObject slowDownArea = null;

	protected override void HitTarget()
	{
		DragonStats dragonStats = (DragonStats)stats;

		Vector3 pos = transform.position;
		pos.y += 1f;

		Enemy firstTarget = target.GetComponent<Enemy>();

		if (firstTarget != null)
		{
			firstTarget.TakeDamage(stats.AttackStrength);
			GameObject area = Instantiate(slowDownArea, transform.position, slowDownArea.transform.rotation);
			area.GetComponent<SlowDownArea>().SetRadius(dragonStats.currentDamageArea);
			Destroy(area, dragonStats.currentSlowDownAreaLifeTime);
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
		DragonStats dragonStats = (DragonStats)stats;
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, dragonStats.currentDamageArea);
	}
}
