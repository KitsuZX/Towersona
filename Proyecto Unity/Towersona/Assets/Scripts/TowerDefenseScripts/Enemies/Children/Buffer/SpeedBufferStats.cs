using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Enemy/Buffers/SpeedBuffer")]
public class SpeedBufferStats : BufferStats
{
	[Header("Speed Buffer Stats")]
	[Tooltip("Proporcional: 0 -> No se mueve | 1 -> Velocidad normal | 2 -> El doble | etc")] public float speedBuff;
	//[Tooltip("Cantidad de vida que cura")] public float healingBuff;
	//[Tooltip("Aumento del nº de vidas que quita el enemigo al llegar al final")] public int damageBuff;
}
