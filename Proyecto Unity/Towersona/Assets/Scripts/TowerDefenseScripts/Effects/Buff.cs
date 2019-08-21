using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : TemporalEffect
{
    private float time;
    private BufferStats stats;
    private Enemy enemy;

    private float countdown = 0f;

    public void Initialize(BufferStats stats, Enemy enemy)
    {        
        this.stats = stats;
        this.enemy = enemy;

		effectType = TemporalEffectType.Buff;
        time = stats.buffDuration;
        countdown = time;
    }

	public override void ApplyEffect()
	{
		applied = true;
		throw new System.NotImplementedException();
	}

	public override void RemoveEffect()
	{
		applied = false;
		throw new System.NotImplementedException();
	}
}
