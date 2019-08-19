using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

	[HideInInspector]
	public float currentAttackStrength;
    [HideInInspector]
    public float currentAttackSpeed;
    [HideInInspector]
    public float currentAttackRange;
    [HideInInspector]
	public float currentBulletSpeed;

	private Dictionary<BoostLaser, float> attackStrengthBonusses = new Dictionary<BoostLaser, float>();
	private Dictionary<BoostLaser, float> attackSpeedBonusses = new Dictionary<BoostLaser, float>();
	private Dictionary<BoostLaser, float> happinessBonusses = new Dictionary<BoostLaser, float>();

	[HideInInspector]
	public float AttackStrength {
		get {
			if(attackStrengthBonusses.Count == 0)
			{
				return currentAttackStrength;
			}

			return currentAttackStrength * attackStrengthBonusses.Values.Max();
		}
	}
	public float AttackSpeed
	{
		get
		{
			if (attackSpeedBonusses.Count == 0)
			{
				return currentAttackSpeed;
			}

			return currentAttackSpeed * attackSpeedBonusses.Values.Max();
		}
	}
	public float HappinessBonus
	{
		get
		{
			if (happinessBonusses.Count == 0)
			{
				return 0;
			}

			return happinessBonusses.Values.Max();
		}
	}

	[HideInInspector]
    public TowersonaNeeds needs;

	protected void Update(){}

	public void SetAttackStrengthBoost(BoostLaser laser, float attackBoost)
	{
		if (attackStrengthBonusses.ContainsKey(laser))
		{
			attackStrengthBonusses[laser] = attackBoost;
		}
		else
		{
			attackStrengthBonusses.Add(laser, attackBoost);
		}
		
	}

	public void SetAttackSpeedBoost(BoostLaser laser, float speedBoost)
	{
		if (attackSpeedBonusses.ContainsKey(laser))
		{
			attackSpeedBonusses[laser] = speedBoost;
		}
		else
		{
			attackSpeedBonusses.Add(laser, speedBoost);
		}
	}

	public void SetHappinessBoost(BoostLaser laser, float happinessBoost)
	{
		if (happinessBonusses.ContainsKey(laser))
		{
			happinessBonusses[laser] = happinessBoost;
		}
		else
		{
			happinessBonusses.Add(laser, happinessBoost);
		}
	}

	public void RemoveAttackStrengthBoost(BoostLaser laser)
	{
		attackStrengthBonusses.Remove(laser);
	}
	public void RemoveAttackSpeedBoost(BoostLaser laser)
	{
		attackSpeedBonusses.Remove(laser);
	}
	public void RemoveHappinessBoost(BoostLaser laser)
	{
		happinessBonusses.Remove(laser);
	}

	public abstract void UpdateStats();
    public abstract void SetDefaultValues();   
}
