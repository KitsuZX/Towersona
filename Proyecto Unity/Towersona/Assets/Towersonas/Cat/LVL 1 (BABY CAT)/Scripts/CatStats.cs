using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Cat")]
public class CatStats : TowersonaStats
{
	[Header("Money")]
	[SerializeField][Tooltip("Cada cuanto tiempo dan dinero, en segunods")] private Vector2 timeSpanBetweenGivingMoney;
	[SerializeField][Tooltip("Cúanto dinero dan cada vez que dan dinero")] private Vector2 moneyGiven;

	[Header("Boost")]
	[SerializeField] [Tooltip("Rango al que tienen que estar las torres para recibir los buffos")] private Vector2 boostRange;

	[Header("Love Boost")]
	[SerializeField][Tooltip("0 -> Le baja a velocidad normal | 1 -> No le baja la felicidad")] private Vector2 happinessBoost;

	[Header("Attack Boost")]
	[SerializeField][Tooltip("Proporcional: 0 es que no hace nada, 1 es lo normal, 2 el doble, etc.")] private Vector2 attackStrengthBoost;
	[SerializeField][Tooltip("Proporcional: 0 es que no hace nada, 1 es lo normal, 2 el doble, etc.")] private Vector2 attackSpeedBoost;

	[HideInInspector]
	public float currentTimeSpan;
	[HideInInspector]
	public int currentMoneyGiven;
	[HideInInspector]
	public float currentHappinessBoost;
	[HideInInspector]
	public float currentBoostRange;
	[HideInInspector]
	public float currentAttackStrengthBoost;
	[HideInInspector]
	public float currentAttackSpeedBoost;

	public override void UpdateStats()
    {     
		currentTimeSpan = Mathf.Lerp(timeSpanBetweenGivingMoney.x, timeSpanBetweenGivingMoney.y, needs.HappinessLevel);
		currentMoneyGiven = (int) Mathf.Lerp(moneyGiven.x, moneyGiven.y, needs.HappinessLevel);

		currentBoostRange = Mathf.Lerp(boostRange.x, boostRange.y, needs.HappinessLevel);
		currentHappinessBoost = Mathf.Lerp(happinessBoost.x, happinessBoost.y, needs.HappinessLevel);

		currentAttackStrengthBoost = Mathf.Lerp(attackStrengthBoost.x, attackStrengthBoost.y, needs.HappinessLevel);
		currentAttackSpeedBoost = Mathf.Lerp(attackSpeedBoost.x, attackSpeedBoost.y, needs.HappinessLevel);
	}

    public override void SetDefaultValues()
    {
		currentMoneyGiven = (int) moneyGiven.y;

		currentBoostRange = boostRange.y;
		currentHappinessBoost = happinessBoost.y;

		currentAttackStrengthBoost = attackStrengthBoost.y;
		currentAttackSpeedBoost = attackSpeedBoost.y;
	}
}
