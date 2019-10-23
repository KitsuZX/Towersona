using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649
public class Buffer : Enemy
{    
	[SerializeField] private BufferClass bufferClass;

	private BufferStats stats;
	private float countdown;

	private void Awake()
	{
		stats = (BufferStats)enemyStats;
		countdown = stats.timeBetweenBuffs;
	}

	protected override void Update()
	{
		base.Update();

		if(countdown <= 0)
		{
			ReleaseBuff();
			countdown = stats.timeBetweenBuffs;
		}

		countdown -= Time.deltaTime;

	}

	private void ReleaseBuff()
	{		
		Collider[] colliders = Physics.OverlapSphere(transform.position, stats.range);

		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy")) { 
				Enemy e = collider.GetComponent<Enemy>();
				if (e != null && e != this)
				{
					ApplyBuff(e);
				}
			}
		}
	}

	private void ApplyBuff(Enemy target)
	{
		switch (bufferClass)
		{
			case BufferClass.Healer:
				HealingBufferStats healingBufferStats = (HealingBufferStats)stats;
				target.Heal(healingBufferStats.healingBuff);			
				break;
			case BufferClass.Speeder:
				SpeedBufferStats speedBufferStats = (SpeedBufferStats)stats;
				SpeedBoost speedBoost = (SpeedBoost)TemporalEffect.CreateEffect(TemporalEffectType.SpeedBoost);
				speedBoost.Initialize(speedBufferStats.speedBuff, stats.buffDuration, target.gameObject);
				speedBoost.ApplyEffect();
				break;
			case BufferClass.Damager:
                DamageBufferStats damageBufferStats = (DamageBufferStats)stats;
                target.Strengthen(damageBufferStats.damageBuff);
				break;
		}
	}

	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(transform.position, stats.range);
		}
	}

	enum BufferClass
	{
		Healer, Speeder, Damager
	}
}
