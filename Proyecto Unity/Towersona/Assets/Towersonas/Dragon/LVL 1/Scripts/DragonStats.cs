using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Towersonas/Dragon/RegularBoringDragon")]
public class DragonStats : TowersonaStats
{
	[Header("Damage")]
	[SerializeField] private Vector2 bulletDamage = Vector2.zero;
	[SerializeField] private Vector2 attackSpeed = Vector2.zero;
	[SerializeField] private Vector2 range = Vector2.zero;
	[SerializeField] private Vector2 bulletSpeed = Vector2.zero;

	[Header("Dragon Stats")]
	[SerializeField][Tooltip("Area en el que la bala hace daño")] private Vector2 damageArea = Vector2.zero;
	[SerializeField] [Tooltip("La proporción de daño que reciben los enemigos que no están en el centro 0 -> No reciben daño | 1 -> Reciben todo el daño")] private Vector2 areaDamageReduction = Vector2.zero;

	[Header("Cone Area Stats")]
	[SerializeField] [Tooltip("Ancho del cono. Si no ataca en cono, puedes poner lo que quieras")] private Vector2 damageAreaWidth = Vector2.zero;
	[SerializeField] [Tooltip("Tiempo que un enemigo está quemado")] private Vector2 burnTime = Vector2.zero;

	[Header("Slow down Area")]
	[Tooltip("0 -> No le afecta el slow | 1 -> No le mueve ni Dios")][SerializeField] Vector2 slowDownPercentage = Vector2.zero;
	[Tooltip("Tiempo en segundos que dura el slow")][SerializeField] private Vector2 slowDownTime = Vector2.zero;
	[Tooltip("Tiempo en segundos que dura el área")][SerializeField] private Vector2 slowDownAreaLifeTime = Vector2.zero;

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
