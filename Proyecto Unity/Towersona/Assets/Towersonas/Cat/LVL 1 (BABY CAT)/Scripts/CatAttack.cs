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
	private GameObject moneySum = null;

	private List<GameObject> towersonasInRange;
	private List<BoostLaser> lasers;

	private LineRenderer lineRenderer;

	[HideInInspector] public float currentTimeSpan;
	[HideInInspector] public int currentMoneyGiven;
	[HideInInspector] public float currentHappinessBoost;
	[HideInInspector] public float currentBoostRange;
	[HideInInspector] public float currentAttackStrengthBoost;
	[HideInInspector] public float currentAttackSpeedBoost;

	CatStats catStats;

	protected override void Start()
	{
		base.Start();
		catStats = (CatStats)stats;
		currentMoneyGiven = (int)catStats.moneyGiven.y;

		currentBoostRange = catStats.boostRange.y;
		currentHappinessBoost = catStats.happinessBoost.y;

		currentAttackStrengthBoost = catStats.attackStrengthBoost.y;
		currentAttackSpeedBoost = catStats.attackSpeedBoost.y;

		Invoke("StartGivingMoney", currentTimeSpan);

		towersonasInRange = new List<GameObject>();
		lasers = new List<BoostLaser>();
	}

	protected override void Update()
	{
		base.Update();

		for (int i = 0; i < lasers.Count; i++)
		{			
			lasers[i].UpdateBoosts();			
		}
	}

	private IEnumerator GiveMoney()
	{
		yield return new WaitForSeconds(currentTimeSpan);

		while (true)
		{
			SpawnMoneySum();
			PlayerStats.Instance.AddMoney(currentMoneyGiven);

			yield return new WaitForSeconds(currentTimeSpan);
		}
	}

	public override void Shoot(Transform target)
	{
		GameObject bulletObject = Instantiate(boostLaserPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);
		Bullet bullet = bulletObject.GetComponent<Bullet>();
		bullet.pattern = this;

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
				if (Vector3.Distance(towersona.transform.position, transform.position) < currentBoostRange)
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
		moneySumObject.GetComponentInChildren<TextMeshProUGUI>().text = "+ " + currentMoneyGiven.ToString() + " $";
	}

	public void RemoveLaser(BoostLaser laser)
	{
		if (laser)
		{
			lasers.Remove(laser);
			towersonasInRange.Remove(laser.target);
		}
	}

	public override void UpdateStats()
	{
		base.UpdateStats();
		currentTimeSpan = Mathf.Lerp(catStats.timeSpanBetweenGivingMoney.x, catStats.timeSpanBetweenGivingMoney.y, needs.HappinessLevel);
		currentMoneyGiven = (int)Mathf.Lerp(catStats.moneyGiven.x, catStats.moneyGiven.y, needs.HappinessLevel);

		currentBoostRange = Mathf.Lerp(catStats.boostRange.x, catStats.boostRange.y, needs.HappinessLevel);
		currentHappinessBoost = Mathf.Lerp(catStats.happinessBoost.x, catStats.happinessBoost.y, needs.HappinessLevel);

		currentAttackStrengthBoost = Mathf.Lerp(catStats.attackStrengthBoost.x, catStats.attackStrengthBoost.y, needs.HappinessLevel);
		currentAttackSpeedBoost = Mathf.Lerp(catStats.attackSpeedBoost.x, catStats.attackSpeedBoost.y, needs.HappinessLevel);
	}

	private void StartGivingMoney()
	{

		StartCoroutine("GiveMoney");
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(towersonaLOD.transform.position, currentBoostRange);
		}
	}
}

