using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownAreaBullet : Shooting
{
	[SerializeField] private GameObject slowDownArea = null;

	new DragonAttack pattern;

	private void Start()
	{
		this.pattern = (DragonAttack)base.pattern;
	}

	protected override void HitTarget()
	{
		Vector3 pos = transform.position;
		pos.y += 1f;

		Enemy firstTarget = target.GetComponent<Enemy>();

		if (firstTarget != null)
		{
			firstTarget.TakeDamage(pattern.AttackStrength);
			GameObject area = Instantiate(slowDownArea, transform.position, slowDownArea.transform.rotation);
			SlowDownArea slArea = area.GetComponent<SlowDownArea>();
			slArea.SetRadius(pattern.currentDamageArea);
			slArea.pattern = pattern;
			Destroy(area, pattern.currentSlowDownAreaLifeTime);
		}

		Collider[] colliders = Physics.OverlapSphere(pos, pattern.currentDamageArea);

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				Enemy e = collider.GetComponent<Enemy>();
				if (e != null && e != firstTarget)
				{
					e.TakeDamage(pattern.AttackStrength * pattern.currentAreaDamageReduction);
				}
			}
		}

		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{	
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, pattern.currentDamageArea);
	}
}
