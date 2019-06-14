using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DogAttack : AttackPattern
{
	private List<GameObject> towersonasInRange;
	private List<Laser> lasers;

	private LineRenderer lineRenderer;
	private DogStats dogStats;

	private void Start()
	{
		base.Start();

		dogStats = (DogStats)stats;
		towersonasInRange = new List<GameObject>();
		lasers = new List<Laser>();

		lineRenderer = bulletPrefab.GetComponent<LineRenderer>();
	}

	private void Update()
	{
		base.Update();

		for (int i = 0; i < lasers.Count; i++)
		{
			lasers[i].UpdateHapiness(dogStats.currentLoveRange, dogStats.currentLoveGiven);
		}
	}

	public override void UpdateTarget()
	{
		GameObject[] towersonas = GameObject.FindGameObjectsWithTag("Towersona LOD");

		foreach (GameObject towersona in towersonas)
		{
			if (!towersonasInRange.Contains(towersona) && towersona != gameObject)
			{
				if (Vector3.Distance(towersona.transform.position, transform.position) < dogStats.currentLoveRange) 
				{
					towersonasInRange.Add(towersona);
					Laser laser = Instantiate(bulletPrefab, towersonaLOD.firePoint.position, Quaternion.identity).GetComponent<Laser>();
					laser.gameObject.transform.SetParent(transform);
					laser.SetTarget(towersona, this, transform.position);
					lasers.Add(laser);
				}				
			}		
		}
	}

	public override void Shoot(Transform target)
	{
		
	}

	public void RemoveLaser(Laser laser)
	{
		if (laser) {
			lasers.Remove(laser);
			towersonasInRange.Remove(laser.target);
		}
	}		
}
