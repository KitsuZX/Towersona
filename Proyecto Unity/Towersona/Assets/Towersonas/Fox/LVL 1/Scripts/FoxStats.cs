using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Towersonas/Fox/Regular")]
public class FoxStats : TowersonaStats
{
	[Header("Fox Stats")]
	[Tooltip("0 -> No le afecta el slow | 1 -> No le mueve ni Dios")]
	public Vector2 slowDownPercentage;
	public Vector2 slowDownTime;
}
