using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerBullet : Shooting
{
	[HideInInspector]
	public float explosionRadius = 0f;

	protected override void HitTarget()
	{		
		Vector3 pos = transform.position;
		pos.y += 1f;

		Collider[] colliders = Physics.OverlapSphere(pos, explosionRadius);

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		foreach (Collider collider in colliders)
		{
			if(collider.tag == "Enemy")
			{
				Enemy e = collider.GetComponent<Enemy>();
				if (e != null)
				{
					e.TakeDamage(damage);
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
