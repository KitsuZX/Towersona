using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Dog/Wolf")]
public class WolfStats : DogStats
{
	[Header("Damage")]
	public Vector2 bulletDamage;
	public Vector2 attackSpeed;
	public Vector2 attackRange;
	public Vector2 bulletSpeed;

	public override void UpdateStats()
	{
		base.UpdateStats();
		currentAttackStrength = Mathf.Lerp(bulletDamage.x, bulletDamage.y, needs.HappinessLevel);
		currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
		currentAttackRange = Mathf.Lerp(attackRange.x, attackRange.y, needs.HappinessLevel);
		currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);
	}

	public override void SetDefaultValues()
	{
		base.SetDefaultValues();
		currentAttackStrength = bulletDamage.y;
		currentAttackSpeed = attackSpeed.y;
		currentAttackRange = attackRange.y;
		currentBulletSpeed = bulletSpeed.y;
	}
}
