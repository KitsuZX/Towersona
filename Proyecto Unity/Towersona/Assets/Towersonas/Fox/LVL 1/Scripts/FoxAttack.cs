using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAttack : AttackPattern
{ 
	FoxStats foxStats;

	[SerializeField]
	protected GameObject bulletPrefab;

	[HideInInspector]
	public float currentSlowDownPercentage;
	[HideInInspector]
	public float currentSlowDownTime;

	protected override void Start()
	{
		base.Start();
		foxStats = (FoxStats)stats;
		currentSlowDownPercentage = foxStats.slowDownPercentage.y;
		currentSlowDownTime = foxStats.slowDownTime.y;
	}

	public override void Shoot(Transform target)
	{
		GameObject bulletObject = Instantiate(bulletPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);

		SlowDownBullet bullet = bulletObject.GetComponent<SlowDownBullet>();
		bullet.source = gameObject;
		bullet.pattern = this;

		if (bullet != null) bullet.Seek(target);
	}

	public override void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= currentAttackRange)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}

	public override void UpdateStats()
	{
		base.UpdateStats();
		currentSlowDownPercentage = Mathf.Lerp(foxStats.slowDownPercentage.x, foxStats.slowDownPercentage.y, needs.HappinessLevel);
		currentSlowDownTime = Mathf.Lerp(foxStats.slowDownTime.x, foxStats.slowDownTime.y, needs.HappinessLevel);
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(towersonaLOD.transform.position, currentAttackRange);
		}
	}
}
