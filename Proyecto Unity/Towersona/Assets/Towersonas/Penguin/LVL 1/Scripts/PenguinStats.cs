using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Penguin")]
public class PenguinStats : TowersonaStats
{
	[Header("Damage")]
	public Vector2 bulletDamage;
	public Vector2 attackSpeed;
	public Vector2 range;
	public Vector2 bulletSpeed;

	[Header("Penguin Stats")]
	[Tooltip("0 -> No le afecta el slow | 1 -> No le mueve ni Dios")]
	public Vector2 slowDownPercentage;
	public Vector2 slowDownTime;

	[HideInInspector]
	public float currentSlowDownPercentage;
	[HideInInspector]
	public float currentSlowDownTime;

	public override void UpdateStats()
	{
		currentAttackStrength = Mathf.Lerp(bulletDamage.x, bulletDamage.y, needs.HappinessLevel);
		currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
		currentAttackRange = Mathf.Lerp(range.x, range.y, needs.HappinessLevel);
		currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);
		currentSlowDownPercentage = Mathf.Lerp(slowDownPercentage.x, slowDownPercentage.y, needs.HappinessLevel);
		currentSlowDownTime = Mathf.Lerp(slowDownTime.x, slowDownTime.y, needs.HappinessLevel);
	}

	public override void SetDefaultValues()
	{
		currentAttackStrength = bulletDamage.y;
		currentAttackSpeed = attackSpeed.y;
		currentAttackRange = range.y;
		currentBulletSpeed = bulletSpeed.y;
		currentSlowDownPercentage = slowDownPercentage.y;
		currentSlowDownTime = slowDownTime.y;
	}
}
