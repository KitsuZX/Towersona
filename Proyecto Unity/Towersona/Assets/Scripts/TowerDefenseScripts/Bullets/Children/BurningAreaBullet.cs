using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningAreaBullet : Shooting
{
	DragonStats dragonStats;
	BoxCollider boxCollider;
	

	public override void Seek(Transform _target) {

		dragonStats = (DragonStats)stats;

		base.Seek(_target);

		Vector3 targetPosition = target.position;
		targetPosition.y = transform.position.y;
		transform.LookAt(targetPosition);

		transform.localScale = Vector3.one * dragonStats.currentDamageArea;

		/*GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		

		foreach (GameObject e in enemies)
		{
			if (collider.bounds.Contains(e.transform.position))
			{
				Debug.Log("Quemando peña");
			}
		}*/

		BoxCollider collider = GetComponentInChildren<BoxCollider>();
		Collider[] colliders = Physics.OverlapBox(transform.position, collider.size, transform.localRotation);

		foreach (Collider c in colliders)
		{
			if (c.transform.tag == "Enemy")
			{
				c.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
			}
		}

		Destroy(gameObject, 1f);
	}

	private void Update()
	{
		
	}

	protected override void HitTarget()
	{
		/*Vector3 pos = transform.position;
		pos.y += 1f;

		Enemy firstTarget = target.GetComponent<Enemy>();

		if (firstTarget != null)
		{
			firstTarget.TakeDamage(stats.AttackStrength);
		}

		Collider[] colliders = Physics.OverlapSphere(pos, dragonStats.currentDamageArea);

		BuildManager.Instance.SpawnEffect(impactEffect, pos);

		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				Enemy e = collider.GetComponent<Enemy>();
				if (e != null && e != firstTarget)
				{
					e.TakeDamage(stats.AttackStrength * dragonStats.currentAreaDamageReduction);
				}

			}
		}

		Destroy(gameObject);*/
	}
	
	private void OnDrawGizmos()
	{
		/*Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);*/

		BoxCollider collider = GetComponentInChildren<BoxCollider>();
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, collider.size);
	}
}
