using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Enemy/Buffer")]
public class BufferStats : EnemyStats
{
    [Header("Buffer Stats")]
    [Tooltip("Proporcional: 0 -> No se mueve | 1 -> Velocidad normal | 2 -> El doble | etc")]public float speedBuff;
    [Tooltip("Cantidad de vida que cura")]public float healingBuff;
    [Tooltip("Aumento del nº de vidas que quita el enemigo al llegar al final")]public int damageBuff;


    [Tooltip("Cada cuanto tiempo suelta el buff, en segundos")] public float timeBetweenBuffs;
    [Tooltip("Tiempo que dura el buff")] public float buffDuration;
    [Tooltip("Rango")] public float range;
}
 