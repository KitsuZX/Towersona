using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Fox")]
public class FoxStats : TowersonaStats
{
	[Header("Damage")]
	public Vector2 bulletDamage;
	public Vector2 attackSpeed;
	public Vector2 range;
	public Vector2 bulletSpeed;

	[Header("Fox Stats")]
	public Vector2 slowDownAmout;
	public Vector2 slowDownTime;

	[HideInInspector]
	public float currentSlowDownAmount;
	[HideInInspector]
	public float currentSlowDownTime;

	public override void UpdateStats()
	{
		currentAttackStrength = Mathf.Lerp(bulletDamage.x, bulletDamage.y, needs.HappinessLevel);
		currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
		currentAttackRange = Mathf.Lerp(range.x, range.y, needs.HappinessLevel);
		currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);
		currentSlowDownAmount = Mathf.Lerp(slowDownAmout.x, slowDownAmout.y, needs.HappinessLevel);
		currentSlowDownTime = Mathf.Lerp(slowDownTime.x, slowDownTime.y, needs.HappinessLevel);
	}

	public override void SetDefaultValues()
	{
		currentAttackStrength = bulletDamage.y;
		currentAttackSpeed = attackSpeed.y;
		currentAttackRange = range.y;
		currentBulletSpeed = bulletSpeed.y;
		currentSlowDownAmount = slowDownAmout.y;
		currentSlowDownTime = slowDownTime.y;
	}
}
