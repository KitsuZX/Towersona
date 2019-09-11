using BezierSolution;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
	#region Variables
	public float Speed {
		get
		{
			if(!temporalEffects.ContainsKey(TemporalEffectType.SlowDown) && !temporalEffects.ContainsKey(TemporalEffectType.SpeedBoost))
			{
				return speed;
			}

			float totalSlowDownAmount = 0;
			float totalSpeedBoostAmount = 1;


			if (temporalEffects.ContainsKey(TemporalEffectType.SlowDown)){
				SlowDown slowDown = (SlowDown)temporalEffects[TemporalEffectType.SlowDown];
				totalSlowDownAmount = slowDown.slowDownAmount;
			}

			if (temporalEffects.ContainsKey(TemporalEffectType.SpeedBoost))
			{
				SpeedBoost speedBoost = (SpeedBoost)temporalEffects[TemporalEffectType.SpeedBoost];
				totalSpeedBoostAmount = speedBoost.speedBoostAmount;
			}


			return speed * (1 - totalSlowDownAmount) * (totalSpeedBoostAmount);
		}
	}
    public bool Flies {
        get
        {
            return enemyStats.flies;
        }
    }	

	[SerializeField] public EnemyStats enemyStats = null;
    [SerializeField] private GameObject deathEffect = null;
	[SerializeField] private GameObject healEffect = null;
    [SerializeField] private GameObject strengthenEffect = null;
	[SerializeField] private UnityEvent OnDamageTaken = null;

	[HideInInspector] public float speed;
	[HideInInspector] public float lifes;
	[HideInInspector] public int damage;
	[HideInInspector] public int value;

	private BezierWalkerWithSpeed bezierWalkerWithSpeed;	
	private Dictionary<TemporalEffectType, TemporalEffect> temporalEffects = new Dictionary<TemporalEffectType, TemporalEffect>();
	private MeshRenderer[] meshRenderers;
	private Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
	
	#endregion

	private void Awake()
	{	
		meshRenderers = GetComponentsInChildren<MeshRenderer>();

		speed = enemyStats.initialSpeed;
		lifes = enemyStats.initialLifes;
		damage = enemyStats.initialDamage;
		value = enemyStats.initialValue;

		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			foreach (Material m in meshRenderer.materials)
			{
				originalColors.Add(m, m.color);				
			}
		}
	}

	protected virtual void Update()
	{
		if (!bezierWalkerWithSpeed)
		{
			bezierWalkerWithSpeed = GetComponent<BezierWalkerWithSpeed>();
		}
		else
		{
			bezierWalkerWithSpeed.speed = Speed;
		}	

		foreach(KeyValuePair<TemporalEffectType, TemporalEffect> entry in temporalEffects.ToList())
		{
			entry.Value.Update();
		}
	}

	#region Paths
	public void EndPath()
    {
        KillEnemy(true);
        PlayerStats.Instance.LoseLive(damage);
        
    }
	#endregion

	#region Damage
	private void KillEnemy(bool endPath = false)
    {      
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        if (!endPath)
        {            
            BuildManager.Instance.SpawnEffect(deathEffect, pos);
            PlayerStats.Instance.AddMoney(value);
        }

        Destroy(gameObject);

        LevelManager.Instance.enemiesAlive--;
    }

    public void TakeDamage(float amount) {
        lifes -= amount;
		OnDamageTaken.Invoke();
		if (lifes <= 0)
        {
            KillEnemy();
        }
    }
	#endregion

	#region TemporalEffects
	public void AddTemporalEffect(TemporalEffect newEffect)
	{		
		//Check if this effect is alredy affecting the enemy
		if(!temporalEffects.ContainsKey(newEffect.effectType))
		{
			temporalEffects.Add(newEffect.effectType, newEffect);
		}			
	}
	
	public void RemoveTemporalEffect(TemporalEffect effect)
	{		
		if (temporalEffects.ContainsKey(effect.effectType))
		{
			temporalEffects.Remove(effect.effectType);
		}		
	}

	public bool IsAffactedByEffect(TemporalEffectType effectType)
	{
		return temporalEffects.ContainsKey(effectType);
	}

	public void ResetEffectCountdown(TemporalEffectType effectType)
	{
		temporalEffects[effectType].ResetTimer();
	}

	public TemporalEffect GetEffect(TemporalEffectType effectType)
	{
		return temporalEffects[effectType];
	}

	public void Heal(float amount)
	{
        if (lifes < enemyStats.initialLifes)
        {
            GameObject healEffectObject = Instantiate(healEffect, transform.position, Quaternion.identity);
            lifes += amount;
            Mathf.Clamp(lifes, 0, enemyStats.initialLifes);
            Destroy(healEffectObject, 2f);
        }
	}

    public void Strengthen(int amount)
    {
        GameObject strengthenEffectObject = Instantiate(strengthenEffect, transform.position, Quaternion.identity);
        damage += amount;       
        Destroy(strengthenEffectObject, 2f);
    }
	#endregion
	
	#region Tints	
	public void Tint(Color color)
	{
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			foreach(Material m in meshRenderer.materials)
			{			
				m.SetColor("_Color", color);
			}			
		}
	}

	public void RemoveTint()
	{
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			for (int j = 0; j < meshRenderers[i].materials.Length; j++)
			{
				meshRenderers[i].materials[j].SetColor("_Color", originalColors[meshRenderers[i].materials[j]]);
			}
		}
	}
    #endregion   
}
