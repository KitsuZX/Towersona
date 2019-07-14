using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Dragon/RegularBoringDragon")]
public class DragonStats : TowersonaStats
{
	[Header("Damage")]
	[SerializeField] private Vector2 bulletDamage;
	[SerializeField] private Vector2 attackSpeed;
	[SerializeField] private Vector2 range;
	[SerializeField] private Vector2 bulletSpeed;

	[Header("Dragon Stats")]
	[SerializeField][Tooltip("Area en el que la bala hace daño")] private Vector2 damageArea;
	[SerializeField] [Tooltip("La proporción de daño que reciben los enemigos que no están en el centro 0 -> No reciben daño | 1 -> Reciben todo el daño")] private Vector2 areaDamageReduction;

	[HideInInspector]
	public float currentDamageArea;
	[HideInInspector]
	public float currentAreaDamageReduction;


	public override void UpdateStats()
	{
		currentAttackStrength = Mathf.Lerp(bulletDamage.x, bulletDamage.y, needs.HappinessLevel);
		currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
		currentAttackRange = Mathf.Lerp(range.x, range.y, needs.HappinessLevel);
		currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);
		currentDamageArea = Mathf.Lerp(damageArea.x, damageArea.y, needs.HappinessLevel);
		currentAreaDamageReduction = Mathf.Lerp(areaDamageReduction.x, areaDamageReduction.y, needs.HappinessLevel);
	}

	public override void SetDefaultValues()
	{
		currentAttackStrength = bulletDamage.y;
		currentAttackSpeed = attackSpeed.y;
		currentAttackRange = range.y;
		currentBulletSpeed = bulletSpeed.y;
		currentBulletSpeed = damageArea.y;
		currentAreaDamageReduction = areaDamageReduction.y;
	}
}
