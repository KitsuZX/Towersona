using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowersonaStats : ScriptableObject
{
    [Header("Money")]
    public int buyCost;
    public int sellCost;
    [Header("Towersonsa stats")]
    [Tooltip("Si esta towersona ataca a los enemigos voladores")]
    public bool attacksFliers;

    [Header("Tamagochi stats")]
    [Tooltip("El tamaño del estómago de la towersona")]
    public float maxFood;
    [Tooltip("Hambre que pierde por segundo la towersona")]
    public float foodDecayPerSecond;
    [Tooltip("Amor que pierde por segundo la towersona")]
    public float loveDecayPerSecond;
    [Tooltip("Cuanta felicidad gana la towersona por caricia")]
    public float happinessPerPet;


    [Header("Damage")]
	public Vector2 bulletDamage = Vector2.zero;
	public Vector2 attackSpeed = Vector2.zero;
	public Vector2 range = Vector2.zero;
	public Vector2 bulletSpeed = Vector2.zero;

	[HideInInspector]
    public TowersonaNeeds needs;

	protected void Update(){} 
}
