using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Towersonas/Dragon/RegularBoringDragon")]
public class DragonStats : TowersonaStats
{
	[Header("Dragon Stats")]
	[Tooltip("Area en el que la bala hace daño")] public Vector2 damageArea = Vector2.zero;
	[Tooltip("La proporción de daño que reciben los enemigos que no están en el centro 0 -> No reciben daño | 1 -> Reciben todo el daño")] public Vector2 areaDamageReduction = Vector2.zero;

	[Header("Cone Area Stats")]
	[Tooltip("Ancho del cono. Si no ataca en cono, puedes poner lo que quieras")] public Vector2 damageAreaWidth = Vector2.zero;
	[Tooltip("Tiempo que un enemigo está quemado")] public Vector2 burnTime = Vector2.zero;

	[Header("Slow down Area")]
	[Tooltip("0 -> No le afecta el slow | 1 -> No le mueve ni Dios")] public Vector2 slowDownPercentage = Vector2.zero;
	[Tooltip("Tiempo en segundos que dura el slow")] public Vector2 slowDownTime = Vector2.zero;
	[Tooltip("Tiempo en segundos que dura el área")] public Vector2 slowDownAreaLifeTime = Vector2.zero;
	
}
