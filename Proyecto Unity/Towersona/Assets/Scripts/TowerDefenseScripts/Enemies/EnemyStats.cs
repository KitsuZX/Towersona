using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Enemy/Regular")]
public class EnemyStats : ScriptableObject
{  
	[Header("Enemy Stats")]
    [SerializeField] private float initialSpeed = 2f;
    public float initialLifes = 30f;   
    [Tooltip("Number of lifes the player loses if this enemy reaches the end")] [SerializeField] private int initialDamage = 1;
    [Tooltip("Dinero que da al morir")] [SerializeField] private int initialValue = 20;

    [HideInInspector] public float speed;
	[HideInInspector] public float lifes;
    [HideInInspector] public int damage;
    [HideInInspector] public int value;

    public bool flies;

    public void Initialize()
    {
        speed = initialSpeed;
        lifes = initialLifes;
        damage = initialDamage;
        value = initialValue;
    }
}
