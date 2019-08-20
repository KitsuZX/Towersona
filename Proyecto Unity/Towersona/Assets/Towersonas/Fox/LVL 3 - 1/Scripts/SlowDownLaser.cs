using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownLaser : MonoBehaviour
{
	[SerializeField] protected ParticleSystem impactEffect;

	[HideInInspector] public GameObject target;
	private LineRenderer lineRenderer;

	private Vector3 centre;
	private FoxSlowDownAreaAttack foxAttack;
	private FoxStats stats;
	private Enemy enemy;
	private SlowDown slowDown;

	protected void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		if (target) {
			lineRenderer.SetPosition(1, target.transform.position);
			Vector3 dir = transform.position - target.transform.position;
			impactEffect.transform.position = target.transform.position + dir.normalized;
			impactEffect.transform.rotation = Quaternion.LookRotation(dir);
		}

		CheckSlowDown();

	}

	public void SetTarget(GameObject target, FoxSlowDownAreaAttack catAttack, Vector3 centre)
	{
		this.target = target;
		this.foxAttack = catAttack;

		stats = (FoxStats)GetComponentInParent<Towersona>().stats;
		enemy = target.GetComponent<Enemy>();

		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, target.transform.position);

		this.centre = centre;
	}

	public void CheckSlowDown()
	{
		/*if (target == null || Vector3.Distance(target.transform.position, centre) > stats.currentAttackRange)
		{
			enemy.RemoveSlowDown(slowDown);
			foxAttack.RemoveLaser(this);
			Destroy(gameObject);
		}
		else
		{
			if (!enemy.AlredySlownDownByTowersona(gameObject))
			{
				slowDown = enemy.AddSlowDown(stats.currentSlowDownPercentage, Mathf.Infinity, SlowDownType.Fox, gameObject);
			}
		}*/
	}
}
