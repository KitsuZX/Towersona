using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Dog/AttackBoostDog")]
public class AttackBoostDogStats : DogStats
{
	public Vector2 attackBoost;

	[HideInInspector]
	public float currentAttackBoost;

	public override void UpdateStats()
	{
		base.UpdateStats();
		currentAttackBoost = Mathf.Lerp(attackBoost.x, attackBoost.y, needs.HappinessLevel);		
	}

	public override void SetDefaultValues()
	{
		base.SetDefaultValues();
		currentAttackBoost = attackBoost.y;		
	}
}
