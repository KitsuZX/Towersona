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
    [Tooltip("Cuanta felicidad gana la towersona por caricia")]
    public float happinessPerPet;
    [Tooltip("Hambre que pierde por segundo la towersona")]
    public float hungerPerSecond;
    [Tooltip("Amor que pierde por segundo la towersona")]
    public float hapinessLossPerSecond;
    [Tooltip("Cuanto cae el amor por cada mierda que hay sin recoger")]
    public float angerPerShit;
    public float timeBetweenShits;
    public int maxShits;

	[Header("Damage")]
	public Vector2 bulletDamage = Vector2.zero;
	public Vector2 attackSpeed = Vector2.zero;
	public Vector2 range = Vector2.zero;
	public Vector2 bulletSpeed = Vector2.zero;

	[HideInInspector]
    public TowersonaNeeds needs;

	protected void Update(){} 
}
