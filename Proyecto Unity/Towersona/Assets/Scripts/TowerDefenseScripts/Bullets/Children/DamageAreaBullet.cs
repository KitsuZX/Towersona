using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaBullet : Shooting
{
	[HideInInspector]
	public float explosionRadius = 0f;

	DragonAttack dragonAttack;

	private void Start()
	{
		dragonAttack = (DragonAttack)base.pattern;
	}

	protected override void HitTarget()
	{		

		Vector3 pos = transform.position;
		pos.y += 1f;

		Enemy firstTarget = target.GetComponent<Enemy>();

		if(firstTarget != null)
		{
			firstTarget.TakeDamage(dragonAttack.AttackStrength);
		}

		Collider[] colliders = Physics.OverlapSphere(pos, dragonAttack.currentDamageArea);

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				Enemy e = collider.GetComponent<Enemy>();
				if (e != null && e != firstTarget)
				{
					if ((e.Flies && dragonAttack.stats.attacksFliers) || !e.Flies)
					{
						e.TakeDamage(dragonAttack.AttackStrength * dragonAttack.currentAreaDamageReduction);
					}
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
