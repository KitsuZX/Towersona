using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Dog")]
public class DogStats : TowersonaStats
{
	public Vector2 range;

	public override void UpdateStats()
	{
		currentAttackRange = Mathf.Lerp(range.x, range.y, needs.HappinessLevel);
	}

	public override void SetDefaultValues()
	{
		currentAttackRange = range.y;
	}
}
