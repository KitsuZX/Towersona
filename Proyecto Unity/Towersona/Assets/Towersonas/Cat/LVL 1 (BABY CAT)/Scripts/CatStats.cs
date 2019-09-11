using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Towersonas/Cat")]
public class CatStats : TowersonaStats
{
	[Header("Money")]
	[Tooltip("Cada cuanto tiempo dan dinero, en segunods")] public Vector2 timeSpanBetweenGivingMoney = Vector2.zero;
	[Tooltip("Cúanto dinero dan cada vez que dan dinero")] public Vector2 moneyGiven = Vector2.zero;

	[Header("Boost")]
	[Tooltip("Rango al que tienen que estar las torres para recibir los buffos")] public Vector2 boostRange = Vector2.zero;

	[Header("Love Boost")]
	[Tooltip("0 -> Le baja a velocidad normal | 1 -> No le baja la felicidad")] public Vector2 happinessBoost = Vector2.zero;

	[Header("Attack Boost")]
	[Tooltip("Proporcional: 0 es que no hace nada, 1 es lo normal, 2 el doble, etc.")] public Vector2 attackStrengthBoost = Vector2.zero;
	[Tooltip("Proporcional: 0 es que no hace nada, 1 es lo normal, 2 el doble, etc.")] public Vector2 attackSpeedBoost = Vector2.zero;
	
   
}
