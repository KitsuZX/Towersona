using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Enemy/Regular")]
public class EnemyStats : ScriptableObject
{  
	[Header("Enemy Stats")]
    public float initialSpeed = 2f;
    public float initialLifes = 30f;   
    [Tooltip("Number of lifes the player loses if this enemy reaches the end")] public int initialDamage = 1;
    [Tooltip("Dinero que da al morir")] public int initialValue = 20;
    
    public bool flies;   
}
