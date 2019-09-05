using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BufferStats : EnemyStats
{ 
	[Header("Buffer Stats")]
    [Tooltip("Cada cuanto tiempo suelta el buff, en segundos")] public float timeBetweenBuffs;
    [Tooltip("Tiempo que dura el buff")] public float buffDuration;
    [Tooltip("Rango en el que da el buff")] public float range;
	
}
 