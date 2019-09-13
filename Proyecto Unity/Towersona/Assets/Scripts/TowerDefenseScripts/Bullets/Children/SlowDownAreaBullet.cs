using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownAreaBullet : Shooting
{
	[SerializeField] private GameObject slowDownArea = null;

	DragonAttack dragonAttack;

	private void Start()
	{
		this.dragonAttack = (DragonAttack)base.pattern;
	}

	protected override void HitTarget()
	{
		Vector3 pos = transform.position;
		pos.y += 1f;

		Enemy firstTarget = target.parent.GetComponent<Enemy>();

		if (firstTarget != null)
		{
			firstTarget.TakeDamage(dragonAttack.AttackStrength);
			GameObject area = Instantiate(slowDownArea, transform.position, slowDownArea.transform.rotation);
			SlowDownArea slArea = area.GetComponent<SlowDownArea>();
			slArea.SetRadius(dragonAttack.currentDamageArea);
			slArea.pattern = dragonAttack;
			Destroy(area, dragonAttack.currentSlowDownAreaLifeTime);
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
					e.TakeDamage(dragonAttack.AttackStrength * dragonAttack.currentAreaDamageReduction);
				}
			}
		}

		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{	
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, dragonAttack.currentDamageArea);
	}
}
