using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Dog/Dog")]
public class DogStats : TowersonaStats
{
	[Header("Dog Stats")]
	public Vector2 loveRange;
	public Vector2 loveGiven;

	[HideInInspector]
	public float currentLoveGiven;
	[HideInInspector]
	public float currentLoveRange;

	public override void UpdateStats()
	{
		currentLoveRange = Mathf.Lerp(loveRange.x, loveRange.y, needs.HappinessLevel);
		currentLoveGiven = Mathf.Lerp(loveGiven.x, loveGiven.y, needs.HappinessLevel);
	}

	public override void SetDefaultValues()
	{
		currentLoveRange = loveRange.y;
		currentLoveGiven = loveGiven.y;
	}
}
