using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporalEffect
{
	public TemporalEffectType effectType;
	public GameObject source;

	protected float initialTime;
	protected float currentTime;
	protected GameObject target;

	protected bool applied = false;

	public void Update()
	{
		if(currentTime != Mathf.Infinity && applied)
		{
			currentTime -= Time.deltaTime;
			if(currentTime < 0)
			{
				RemoveEffect();
			}
		}

		if(target == null)
		{
			RemoveEffect();
		}
	}

	public static TemporalEffect CreateEffect(TemporalEffectType effectType)
	{
		switch (effectType)
		{
			case TemporalEffectType.SlowDown:
				return new SlowDown();			
			case TemporalEffectType.Burn:
				return new Burn();
			case TemporalEffectType.SpeedBoost:
				return new SpeedBoost();			
			default:
				Debug.LogError("Not a a valid effect type");
				return null;
		}
	}	

	public abstract void ApplyEffect();
	public abstract void RemoveEffect();
	public void ResetTimer()
	{
		currentTime = initialTime;
	}
}


public enum TemporalEffectType
{
	SlowDown, Burn, SpeedBoost
}

