using BezierSolution;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
	#region Variables
	public float Speed {
		get
		{
			if(!temporalEffects.ContainsKey(TemporalEffectType.SlowDown))
			{
				return enemyStats.speed;
			}						

			SlowDown slowDown = (SlowDown)temporalEffects[TemporalEffectType.SlowDown];
			float totalSlowDownAmount = slowDown.slowDownAmount;

			return enemyStats.speed * (1 - totalSlowDownAmount);
		}
	}
    public bool Flies {
        get
        {
            return enemyStats.flies;
        }
    }
	

	[SerializeField] protected EnemyStats enemyStats = null;
    [SerializeField] private GameObject deathEffect = null;	
	private BezierWalkerWithSpeed bezierWalkerWithSpeed;	
	private Dictionary<TemporalEffectType, TemporalEffect> temporalEffects = new Dictionary<TemporalEffectType, TemporalEffect>();
	private MeshRenderer[] meshRenderers;
	private Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
	#endregion

	private void Awake()
	{
		enemyStats.Initialize();
		meshRenderers = GetComponentsInChildren<MeshRenderer>();


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
        PlayerStats.Instance.LoseLive(enemyStats.damage);
        
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
            PlayerStats.Instance.AddMoney(enemyStats.value);
        }

        Destroy(gameObject);

        LevelManager.Instance.enemiesAlive--;
    }

    public void TakeDamage(float amount) {
        enemyStats.lifes -= amount;
        if(enemyStats.lifes <= 0)
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
