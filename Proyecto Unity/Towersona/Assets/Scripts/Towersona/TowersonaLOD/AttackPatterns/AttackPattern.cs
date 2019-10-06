using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public abstract class AttackPattern : MonoBehaviour
{
	[HideInInspector]
	public Transform target;
	private Enemy enemyTarget;

	protected TowersonaLOD towersonaLOD;
	protected TowersonaLODAnimation animations;

	protected TowersonaNeeds needs;
	[HideInInspector] public TowersonaStats stats;

	private float fireCountdown = 1f;

	private Dictionary<BoostLaser, float> attackStrengthBonusses = new Dictionary<BoostLaser, float>();
	private Dictionary<BoostLaser, float> attackSpeedBonusses = new Dictionary<BoostLaser, float>();
	private Dictionary<BoostLaser, float> happinessBonusses = new Dictionary<BoostLaser, float>();

	[HideInInspector]
	public float currentAttackStrength;
	[HideInInspector]
	public float currentAttackSpeed;
	[HideInInspector]
	public float currentAttackRange;
	[HideInInspector]
	public float currentBulletSpeed;


	[HideInInspector]
	public float AttackStrength
	{
		get
		{
			if (attackStrengthBonusses.Count == 0)
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


	protected virtual void Start()
	{
		towersonaLOD = GetComponent<TowersonaLOD>();
		animations = GetComponent<TowersonaLODAnimation>();
		needs = towersonaLOD.towersona.towersonaNeeds;
		stats = towersonaLOD.towersona.stats;

		currentAttackStrength = stats.bulletDamage.y;
		currentAttackSpeed = stats.attackSpeed.y;
		currentAttackRange = stats.range.y;
		currentBulletSpeed = stats.bulletSpeed.y;

		InvokeRepeating("UpdateStats", 0f, 1f);
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	protected virtual void Update()
	{
		if (needs == null || stats == null) return;

		CheckAnimations();
		towersonaLOD.LockOnTarget(target);
		CheckIfShouldShoot();
	}

	public virtual void UpdateStats()
	{
		currentAttackStrength = Mathf.Lerp(stats.bulletDamage.x, stats.bulletDamage.y, needs.HappinessLevel);
		currentAttackSpeed = Mathf.Lerp(stats.attackSpeed.x, stats.attackSpeed.y, needs.HappinessLevel);
		currentAttackRange = Mathf.Lerp(stats.range.x, stats.range.y, needs.HappinessLevel);
		currentBulletSpeed = Mathf.Lerp(stats.bulletSpeed.x, stats.bulletSpeed.y, needs.HappinessLevel);
	}
   
    private void CheckAnimations()
    {
        if (target == null)
        {
            animations.Idle();
            return;
        }

        animations.Shoot();
    }

    private void CheckIfShouldShoot()
    { 
        if (fireCountdown <= 0f && target != null)
        {
            enemyTarget = target.GetComponent<Enemy>();
            if (enemyTarget.Flies)
            {
                if (stats.attacksFliers)
                {
                    Shoot(target);
                    fireCountdown = 1f / currentAttackSpeed;
                }
            }

			else
			{
				Shoot(target);
				fireCountdown = 1f / currentAttackSpeed;
			}
        }

        fireCountdown -= Time.deltaTime;
    }

	#region Boosts
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
	#endregion

	public abstract void Shoot(Transform target);
    public abstract void UpdateTarget();  

}

