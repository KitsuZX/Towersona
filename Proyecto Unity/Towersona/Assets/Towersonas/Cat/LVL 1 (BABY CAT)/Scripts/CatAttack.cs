using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatAttack : AttackPattern
{

	[Header("References")]
	[SerializeField]
	protected GameObject boostLaserPrefab;


	[SerializeField]
	private GameObject moneySum;

	private List<GameObject> towersonasInRange;
	private List<BoostLaser> lasers;

	private LineRenderer lineRenderer;

	CatStats catStats;

	private void Start()
	{
		base.Start();
		catStats = (CatStats)stats;

		StartCoroutine("GiveMoney");

		towersonasInRange = new List<GameObject>();
		lasers = new List<BoostLaser>();
	}
	
	private void Update()
	{
		base.Update();

		for (int i = 0; i < lasers.Count; i++)
		{			
			lasers[i].UpdateBoosts();			
		}
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
		GameObject bulletObject = Instantiate(boostLaserPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);
		Bullet bullet = bulletObject.GetComponent<Bullet>();

		bullet.SetStats(catStats);

		if (bullet != null) bullet.Seek(target);
	}

	public override void UpdateTarget()
	{
		GameObject[] towersonas = GameObject.FindGameObjectsWithTag("Towersona LOD");


		//TODO: Esto se puede optimizar si en vez de buscar towersonas todo el rato las towersonas se guardasen en algun lado y simplemente avisase cuando algo cambia.
		foreach (GameObject towersona in towersonas)
		{
			if (!towersonasInRange.Contains(towersona) && towersona != gameObject && !towersona.GetComponent<CatAttack>())
			{
				if (Vector3.Distance(towersona.transform.position, transform.position) < catStats.currentBoostRange)
				{
					towersonasInRange.Add(towersona);
					BoostLaser laser = Instantiate(boostLaserPrefab, towersonaLOD.firePoint.position, Quaternion.identity).GetComponent<BoostLaser>();
					laser.gameObject.transform.SetParent(transform);
					laser.SetTarget(towersona, this, transform.position);
					lasers.Add(laser);
				}
			}
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

	public void RemoveLaser(BoostLaser laser)
	{
		if (laser)
		{
			lasers.Remove(laser);
			towersonasInRange.Remove(laser.target);
		}
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(towersonaLOD.transform.position, catStats.currentBoostRange);
		}
	}
}

