using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownLaser : MonoBehaviour
{
	[SerializeField] protected ParticleSystem impactEffect;

	[HideInInspector] public GameObject target;
	[HideInInspector] public FoxSlowDownAreaAttack pattern;
	private LineRenderer lineRenderer;

	private Vector3 centre;
	private Enemy enemy;

	private SlowDown slowDown = null;

	protected void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		if (target) {
			lineRenderer.SetPosition(1, enemy.modelTransform.position);
			Vector3 dir = transform.position - target.transform.position;
			impactEffect.transform.position = target.transform.position + dir.normalized;
			impactEffect.transform.rotation = Quaternion.LookRotation(dir);
		}
    }

	public void SetTarget(GameObject target, FoxSlowDownAreaAttack catAttack, Vector3 centre)
	{
		this.target = target;
		this.pattern = catAttack;
	
		enemy = target.GetComponent<Enemy>();

		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, enemy.modelTransform.position);
      
        slowDown = (SlowDown)TemporalEffect.CreateEffect(TemporalEffectType.SlowDown);
        slowDown.Initialize(pattern.currentSlowDownPercentage, Mathf.Infinity, target.gameObject);
        slowDown.ApplyEffect();        

        InvokeRepeating("CheckSlowDown", 0f, 0.5f);

		this.centre = centre;
	}

	public void CheckSlowDown()
	{
		//If the target has died or is out of range
		if (enemy == null || Vector3.Distance(target.transform.GetChild(0).position, centre) > pattern.currentAttackRange)
		{
            if (enemy != null)
            {
                slowDown.RemoveEffect();
            }

			pattern.RemoveLaser(this);

			Destroy(gameObject);
			return;
		}			
	}
}
