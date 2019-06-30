using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAttack : AttackPattern
{
	PenguinStats foxStats;

	private void Start()
	{
		base.Start();
		foxStats = (PenguinStats)stats;
	}

	public override void Shoot(Transform target)
	{
		GameObject bulletObject = Instantiate(bulletPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);

		SlowDownBullet bullet = bulletObject.GetComponent<SlowDownBullet>();
		bullet.damage = stats.Strength;
		bullet.speed = stats.currentBulletSpeed;
		bullet.slowDownAmount = foxStats.currentSlowDownPercentage;
		bullet.slowDownTime = foxStats.currentSlowDownTime;

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

		if (nearestEnemy != null && shortestDistance <= stats.currentAttackRange)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}
}
