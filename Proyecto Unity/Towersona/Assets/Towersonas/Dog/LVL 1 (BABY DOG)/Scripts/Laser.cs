using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
	[SerializeField] private ParticleSystem impactEffect;
	private GameObject target;
	private LineRenderer lineRenderer;

	private DogAttack dogAttack;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	public void SetTarget(GameObject target, DogAttack dogAttack)
	{
		this.target = target;
		this.dogAttack = dogAttack;

		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, target.transform.position);

		Vector3 dir = transform.position - target.transform.position;
		impactEffect.transform.position = target.transform.position +  dir.normalized;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);		
	}

	public void CheckIfStillInRange(float range)
	{
		if (target == null || Vector3.Distance(target.transform.position, transform.position) > range)
		{
			dogAttack.RemoveLaser(this);
			Destroy(gameObject);
		}
	}
}
