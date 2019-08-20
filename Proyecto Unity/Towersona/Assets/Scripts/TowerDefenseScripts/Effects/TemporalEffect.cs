using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporalEffect
{
	public TemporalEffectType effectType;
	public GameObject source;

	protected float time;
	protected GameObject target;

	protected bool applied = false;

	public void Update()
	{
		if(time != Mathf.Infinity && applied)
		{
			time -= Time.deltaTime;
			if(time < 0)
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
			case TemporalEffectType.Buff:
				return new Buff();			
			default:
				Debug.LogError("Not a a valid effect type");
				return null;
		}
	}	

	public abstract void ApplyEffect();
	public abstract void RemoveEffect();
}


public enum TemporalEffectType
{
	SlowDown, Burn, Buff
}

