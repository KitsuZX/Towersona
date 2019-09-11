using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoxSlowDownAreaAttack : AttackPattern
{
	private FoxStats foxStats;
	private List<GameObject> enemiesInRange;
	private List<SlowDownLaser> lasers;

	[SerializeField] protected GameObject slowDownLaser = null;
	[SerializeField] protected GameObject bulletPrefab = null;

	[HideInInspector] public float currentSlowDownPercentage;
	
	protected override void Start()
	{
		base.Start();
		foxStats = (FoxStats)stats;
		currentSlowDownPercentage = foxStats.slowDownPercentage.y; ;
		enemiesInRange = new List<GameObject>();
		lasers = new List<SlowDownLaser>();
	}

	public override void Shoot(Transform target)
	{
		GameObject bulletObject = Instantiate(bulletPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);

		Bullet bullet = bulletObject.GetComponent<Bullet>();
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
			if (!enemiesInRange.Contains(enemy))
			{
				if (Vector3.Distance(enemy.transform.position, transform.position) < currentAttackRange)
				{
					enemiesInRange.Add(enemy);
					SlowDownLaser laser = Instantiate(slowDownLaser, towersonaLOD.firePoint.position, Quaternion.identity).GetComponent<SlowDownLaser>();
					laser.gameObject.transform.SetParent(transform);
					laser.pattern = this;
					laser.SetTarget(enemy, this, transform.position);
					lasers.Add(laser);
				}
			}

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

	public void RemoveLaser(SlowDownLaser laser)
	{
		if (laser)
		{
			lasers.Remove(laser);
			enemiesInRange.Remove(laser.target);
		}
	}

	public override void UpdateStats()
	{
		base.UpdateStats();
		currentSlowDownPercentage = Mathf.Lerp(foxStats.slowDownPercentage.x, foxStats.slowDownPercentage.y, needs.HappinessLevel);
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
