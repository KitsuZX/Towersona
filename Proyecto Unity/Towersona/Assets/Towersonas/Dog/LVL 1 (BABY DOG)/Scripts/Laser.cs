using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
	[SerializeField] private ParticleSystem impactEffect;
	[HideInInspector] public GameObject target;
	private LineRenderer lineRenderer;

	private DogAttack dogAttack;
	private TowersonaNeeds needs;
	private Vector3 centre;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	public void SetTarget(GameObject target, DogAttack dogAttack, Vector3 centre)
	{
		this.target = target;
		this.dogAttack = dogAttack;

		needs = this.target.GetComponent<TowersonaLOD>().towersona.towersonaNeeds;

		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, target.transform.position);

		this.centre = centre;

		Vector3 dir = transform.position - target.transform.position;
		impactEffect.transform.position = target.transform.position +  dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);		
	}

	public void UpdateHapiness(float range, float loveGiven)
	{
		if (target == null || Vector3.Distance(target.transform.position, centre) > range)
		{
			//Towersona is out of range, so destroy it
			needs.SetLoveDecayReduction(0);
			dogAttack.RemoveLaser(this);
			Destroy(gameObject);
		}
		else
		{
			needs.SetLoveDecayReduction(loveGiven);
		}
	}
}
