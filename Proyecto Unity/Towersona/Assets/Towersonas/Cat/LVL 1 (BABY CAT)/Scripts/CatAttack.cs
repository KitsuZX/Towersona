using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatAttack : AttackPattern
{
	[SerializeField]
	public GameObject moneySum;

	CatStats catStats;
	private void Start()
	{
		Initialize();
	}

	protected void Initialize()
	{
		base.Start();
		catStats = (CatStats)stats;

		StartCoroutine("GiveMoney");
	}

	private IEnumerator GiveMoney()
	{
		yield return new WaitForSeconds(catStats.currentTimeSpan);

		while (true)
		{
			SpawnMoneySum();
			PlayerStats.Instance.AddMoney(catStats.currentMoneyGiven);

			yield return new WaitForSeconds(catStats.currentTimeSpan);
		}
	}

	public override void Shoot(Transform target)
	{
		GameObject bulletObject = Instantiate(bulletPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);
		Bullet bullet = bulletObject.GetComponent<Bullet>();
		bullet.damage = stats.Strength;
		bullet.speed = stats.currentBulletSpeed;

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

	private void SpawnMoneySum()
	{
		Vector3 pos = transform.position;
		pos.y += 2f;
		pos.z += 1.5f;
		
		GameObject moneySumObject = Instantiate(moneySum, pos, moneySum.transform.rotation);
		moneySumObject.GetComponentInChildren<TextMeshProUGUI>().text = "+ " + catStats.currentMoneyGiven.ToString() + " $";
	}
}

