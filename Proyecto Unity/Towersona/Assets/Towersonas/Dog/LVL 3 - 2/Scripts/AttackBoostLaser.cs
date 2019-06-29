using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostLaser : Laser
{
	private AttackBoostDogStats attackBoostDog;
	private TowersonaStats stats;

	private void Start()
	{
		stats = GetComponentInParent<Towersona>().stats;
		attackBoostDog = (AttackBoostDogStats)stats;
	}

	public new void UpdateHapiness(float range, float loveGiven)
	{	
		if (target == null || Vector3.Distance(target.transform.position, centre) > range)
		{		
			//Towersona is out of range, so destroy it
			needs.SetLoveDecayReduction(0);
			targetStats.SetAttackBoost(0);
	
			dogAttack.RemoveLaser(this);
			Destroy(gameObject);
		}
		else
		{		
			needs.SetLoveDecayReduction(loveGiven);
			targetStats.SetAttackBoost(attackBoostDog.currentAttackBoost);
		}
	}
}
