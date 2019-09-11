﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningAreaBullet : Shooting
{
	DragonStats dragonStats;
	BoxCollider boxCollider;

	new DragonAttack pattern;

	public override void Seek(Transform _target) {

		this.pattern = (DragonAttack)base.pattern;
		boxCollider = GetComponentInChildren<BoxCollider>();

		base.Seek(_target);
		
		float currentZSize = boxCollider.bounds.size.z;
		float currentXSize = boxCollider.bounds.size.x;

		Vector3 scale = transform.localScale;
		scale.x = pattern.currentDamageAreaWidth * scale.x / currentXSize;
		scale.z = pattern.currentAttackRange * scale.z / currentZSize;
		transform.localScale = scale;

		Vector3 targetPosition = target.position;
		targetPosition.y = transform.position.y;
		transform.LookAt(targetPosition);		


		Destroy(gameObject, 1f);
	}

	protected override void Update()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			if (boxCollider.bounds.Contains(enemy.transform.position))
			{
				Enemy e = enemy.GetComponent<Enemy>();

				if (!e.IsAffactedByEffect(TemporalEffectType.Burn))
				{					
					Burn burn = (Burn)TemporalEffect.CreateEffect(TemporalEffectType.Burn);
					burn.Initialize(pattern.currentAttackStrength, pattern.currentBurnTime, enemy, e.gameObject);
					burn.ApplyEffect();
				}
			}
		}
	}

	protected override void HitTarget(){}
	
}
