using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Towersonas/Dragon/RegularBoringDragon")]
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

	[Header("Cone Area Stats")]
	[SerializeField] [Tooltip("Ancho del cono. Si no ataca en cono, puedes poner lo que quieras")] private Vector2 damageAreaWidth;
	[SerializeField] [Tooltip("Tiempo que un enemigo está quemado")] private Vector2 burnTime;

	[Header("Slow don Area")]
	[Tooltip("0 -> No le afecta el slow | 1 -> No le mueve ni Dios")]
	public Vector2 slowDownPercentage;
	public Vector2 slowDownTime;
	public Vector2 slowDownAreaLifeTime;

	[HideInInspector]
	public float currentDamageArea;
	[HideInInspector]
	public float currentAreaDamageReduction;
	[HideInInspector]
	public float currentDamageAreaWidth;
	[HideInInspector]
	public float currentBurnTime;
	[HideInInspector]
	public float currentSlowDownPercentage;
	[HideInInspector]
	public float currentSlowDownTime;
	[HideInInspector]
	public float currentSlowDownAreaLifeTime;

	public override void UpdateStats()
	{
		currentAttackStrength = Mathf.Lerp(bulletDamage.x, bulletDamage.y, needs.HappinessLevel);
		currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
		currentAttackRange = Mathf.Lerp(range.x, range.y, needs.HappinessLevel);
		currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);
		currentDamageArea = Mathf.Lerp(damageArea.x, damageArea.y, needs.HappinessLevel);
		currentAreaDamageReduction = Mathf.Lerp(areaDamageReduction.x, areaDamageReduction.y, needs.HappinessLevel);
		currentDamageAreaWidth = Mathf.Lerp(damageAreaWidth.x, damageAreaWidth.y, needs.HappinessLevel);
		currentBurnTime = Mathf.Lerp(burnTime.x, burnTime.y, needs.HappinessLevel);
		currentSlowDownPercentage = Mathf.Lerp(slowDownPercentage.x, slowDownPercentage.y, needs.HappinessLevel);
		currentSlowDownTime = Mathf.Lerp(slowDownTime.x, slowDownTime.y, needs.HappinessLevel);
		currentSlowDownAreaLifeTime = Mathf.Lerp(slowDownAreaLifeTime.x, slowDownAreaLifeTime.y, needs.HappinessLevel);
	}

	public override void SetDefaultValues()
	{
		currentAttackStrength = bulletDamage.y;
		currentAttackSpeed = attackSpeed.y;
		currentAttackRange = range.y;
		currentBulletSpeed = bulletSpeed.y;
		currentBulletSpeed = damageArea.y;
		currentAreaDamageReduction = areaDamageReduction.y;
		currentDamageAreaWidth = damageAreaWidth.y;
		currentBurnTime = burnTime.y;
		currentSlowDownPercentage = slowDownPercentage.y;
		currentSlowDownTime = slowDownTime.y;
		currentSlowDownAreaLifeTime = slowDownAreaLifeTime.y;
	}
}
