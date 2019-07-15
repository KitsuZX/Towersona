using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownArea : MonoBehaviour
{
	public DragonStats dragonStats;

	private void FixedUpdate()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, dragonStats.currentDamageArea);	

		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				Enemy e = collider.GetComponent<Enemy>();
				if (e != null)
				{
					e.AddSlowDown(dragonStats.currentSlowDownPercentage, dragonStats.currentSlowDownTime, Enemy.SlowDownType.Area, gameObject);
				}

			}
		}
	}

	public void SetRadius(float radius)
	{
		transform.localScale = Vector3.one * radius;
	}

	private void OnDrawGizmos()
	{
		/*Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 1f);*/
	}
}
