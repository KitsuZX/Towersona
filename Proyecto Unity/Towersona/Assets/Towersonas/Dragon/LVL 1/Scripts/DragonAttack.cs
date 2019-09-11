using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;
using System.Linq;

public class DragonAttack : AttackPattern
{
	[Header("References")]
	[SerializeField]
	protected GameObject bulletPrefab;

	[HideInInspector] public float currentDamageArea;
	[HideInInspector] public float currentAreaDamageReduction;
	[HideInInspector] public float currentDamageAreaWidth;
	[HideInInspector] public float currentBurnTime;
	[HideInInspector] public float currentSlowDownPercentage;
	[HideInInspector] public float currentSlowDownTime;
	[HideInInspector] public float currentSlowDownAreaLifeTime;


	DragonStats dragonStats;

	protected override void Start()
	{
		base.Start();

		dragonStats = (DragonStats)stats;
		currentDamageArea = dragonStats.damageArea.y;
		currentAreaDamageReduction = dragonStats.areaDamageReduction.y;
		currentDamageAreaWidth = dragonStats.damageAreaWidth.y;
		currentBurnTime = dragonStats.burnTime.y;
		currentSlowDownPercentage = dragonStats.slowDownPercentage.y;
		currentSlowDownTime = dragonStats.slowDownTime.y;
		currentSlowDownAreaLifeTime = dragonStats.slowDownAreaLifeTime.y;
	}

	public override void UpdateStats()
	{
		base.UpdateStats();

		currentDamageArea = Mathf.Lerp(dragonStats.damageArea.x, dragonStats.damageArea.y, needs.HappinessLevel);
		currentAreaDamageReduction = Mathf.Lerp(dragonStats.areaDamageReduction.x, dragonStats.areaDamageReduction.y, needs.HappinessLevel);
		currentDamageAreaWidth = Mathf.Lerp(dragonStats.damageAreaWidth.x, dragonStats.damageAreaWidth.y, needs.HappinessLevel);
		currentBurnTime = Mathf.Lerp(dragonStats.burnTime.x, dragonStats.burnTime.y, needs.HappinessLevel);
		currentSlowDownPercentage = Mathf.Lerp(dragonStats.slowDownPercentage.x, dragonStats.slowDownPercentage.y, needs.HappinessLevel);
		currentSlowDownTime = Mathf.Lerp(dragonStats.slowDownTime.x, dragonStats.slowDownTime.y, needs.HappinessLevel);
		currentSlowDownAreaLifeTime = Mathf.Lerp(dragonStats.slowDownAreaLifeTime.x, dragonStats.slowDownAreaLifeTime.y, needs.HappinessLevel);
	}

	public override void Shoot(Transform target)
	{
		GameObject bulletObject = Instantiate(bulletPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);

		Shooting bullet = bulletObject.GetComponent<Shooting>();

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
                    if (enemy.progress == greatestProgress)
                    {
                        target = enemy.transform;
                        return;
                    }
                }

                break;
            case PriorizationOption.Closer:
                float shortestDistance = Mathf.Infinity;
                BezierWalkerWithSpeed nearestEnemy = null;

                foreach (BezierWalkerWithSpeed enemy in enemiesInRange)
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

                break;
            case PriorizationOption.Last:
                float lowestProgress = progresses.Min();

                foreach (BezierWalkerWithSpeed enemy in enemiesInRange)
                {
                    if (enemy.progress == lowestProgress)
                    {
                        target = enemy.transform;
                        return;
                    }
                }
                break;
            case PriorizationOption.Random:
                int index = Random.Range(0, enemiesInRange.Count);
                target = enemiesInRange[index].transform;
                break;
        }
    }

    private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(towersonaLOD.transform.position, currentAttackRange);
		}
	}
}
