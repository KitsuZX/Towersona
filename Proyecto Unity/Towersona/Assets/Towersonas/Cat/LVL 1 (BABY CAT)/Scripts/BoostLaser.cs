using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoostLaser : MonoBehaviour
{
	[SerializeField] protected ParticleSystem impactEffect;

	[HideInInspector] public GameObject target;
	private LineRenderer lineRenderer;

	private TowersonaStats targetStats;
	private Vector3 centre;
	private CatAttack catAttack;
	private CatStats stats;

	protected void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();		
	}

	public void SetTarget(GameObject target, CatAttack catAttack, Vector3 centre)
	{
		this.target = target;
		this.catAttack = catAttack;

		stats = (CatStats)GetComponentInParent<Towersona>().stats;
		targetStats = this.target.GetComponent<TowersonaLOD>().towersona.stats;

		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, target.transform.position);

		this.centre = centre;

		Vector3 dir = transform.position - target.transform.position;
		impactEffect.transform.position = target.transform.position + dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}

	public void UpdateBoosts()
	{	
		if (target == null || Vector3.Distance(target.transform.position, centre) > stats.currentBoostRange)
		{
			//Towersona is out of range, so destroy it. Resets all boosts
			targetStats.RemoveAttackStrengthBoost(this);
			targetStats.RemoveAttackSpeedBoost(this);			
			targetStats.RemoveHappinessBoost(this);

			catAttack.RemoveLaser(this);
			Destroy(gameObject);
		}
		else
		{		
			targetStats.SetHappinessBoost(this, stats.currentHappinessBoost);				//Love
			targetStats.SetAttackStrengthBoost(this, stats.currentAttackStrengthBoost);   //Attack Strength
			targetStats.SetAttackSpeedBoost(this, stats.currentAttackSpeedBoost);			//Attack Speed
		}
	}
}
