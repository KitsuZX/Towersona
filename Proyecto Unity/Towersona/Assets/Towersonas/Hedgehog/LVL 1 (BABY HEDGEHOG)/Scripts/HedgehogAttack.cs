using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;
using System.Linq;

public class HedgehogAttack : AttackPattern
{

	[SerializeField]
	protected GameObject bulletPrefab;

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
		Collider[] colliders = Physics.OverlapSphere(transform.position, currentAttackRange);
		List<BezierWalkerWithSpeed> enemiesInRange = new List<BezierWalkerWithSpeed>();
		List<float> progresses = new List<float>();

		foreach (Collider collider in colliders)
		{
			if (collider.gameObject.CompareTag("Enemy"))
			{
				BezierWalkerWithSpeed walker = collider.GetComponent<BezierWalkerWithSpeed>();
				enemiesInRange.Add(walker);
				progresses.Add(walker.progress);
			}
		}

		if (enemiesInRange.Count <= 0)
		{
			target = null;
			return;
		}

		switch (DebuggingOptions.Instance.priorizationOption)
		{
			case PriorizationOption.First:
				float greatestProgress = progresses.Max();

				foreach (BezierWalkerWithSpeed enemy in enemiesInRange)
				{
					Enemy enem = enemy.GetComponent<Enemy>();
					if ((enem.Flies && stats.attacksFliers) || !enem.Flies)
					{
						if (enemy.progress == greatestProgress)
						{
							target = enemy.transform;
							return;
						}
					}
				}

				break;
			case PriorizationOption.Closer:
				float shortestDistance = Mathf.Infinity;
				BezierWalkerWithSpeed nearestEnemy = null;

				foreach (BezierWalkerWithSpeed enemy in enemiesInRange)
				{
					Enemy enem = enemy.GetComponent<Enemy>();
					if ((enem.Flies && stats.attacksFliers) || !enem.Flies)
					{
						float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
						if (distanceToEnemy < shortestDistance)
						{
							shortestDistance = distanceToEnemy;
							nearestEnemy = enemy;
						}

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

				break;
			case PriorizationOption.Last:
				float lowestProgress = progresses.Min();

				foreach (BezierWalkerWithSpeed enemy in enemiesInRange)
				{
					Enemy enem = enemy.GetComponent<Enemy>();
					if ((enem.Flies && stats.attacksFliers) || !enem.Flies)
					{
						if (enemy.progress == lowestProgress)
						{
							target = enemy.transform;
							return;
						}
					}
				}
				break;
			case PriorizationOption.Random:
				while (true)
				{
					int index = Random.Range(0, enemiesInRange.Count);
					Enemy enem = enemiesInRange[index].GetComponent<Enemy>();
					if ((enem.Flies && stats.attacksFliers) || !enem.Flies)
					{

						target = enemiesInRange[index].transform;
						return;
					}
				}
		}
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
