using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowersonaStats : ScriptableObject
{
    [Header("Money")]
    public int buyCost;
    public int sellCost;
    [Header("Towersonsa stats")]
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

	[HideInInspector]
	public float currentAttackStrength;
    [HideInInspector]
    public float currentAttackSpeed;
    [HideInInspector]
    public float currentAttackRange;
    [HideInInspector]
    public float currentBulletSpeed;

	[HideInInspector]
	private float attackBoost = 0;

	[HideInInspector]
	public float Strength {
		get {
			return currentAttackStrength + attackBoost;
		}
	}

	[HideInInspector]
    public TowersonaNeeds needs;  

	protected void Update()
	{
		
	}


	public void SetAttackBoost(float attackBoost)
	{
		this.attackBoost = attackBoost;
	}

	public abstract void UpdateStats();
    public abstract void SetDefaultValues();   
}
