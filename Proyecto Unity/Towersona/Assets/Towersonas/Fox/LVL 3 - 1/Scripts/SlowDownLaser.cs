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

	private SlowDown slowDown = null;

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
		//If the target has died or is out of range
		if (target == null || Vector3.Distance(target.transform.position, centre) > stats.currentAttackRange)
		{
			slowDown.RemoveEffect();
			foxAttack.RemoveLaser(this);

			Destroy(gameObject);
			return;
		}
		
		if (!enemy.IsAffactedByEffect(TemporalEffectType.SlowDown))
		{
			slowDown = (SlowDown)TemporalEffect.CreateEffect(TemporalEffectType.SlowDown);
			slowDown.Initialize(stats.currentSlowDownPercentage, Mathf.Infinity, target.gameObject);
			slowDown.ApplyEffect();
		}		
	}
}
