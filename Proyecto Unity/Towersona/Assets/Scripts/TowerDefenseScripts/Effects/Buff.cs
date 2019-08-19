using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    private float time;
    private BufferStats stats;
    private Enemy enemy;

    private float countdown = 0f;

    public Buff(BufferStats stats, Enemy enemy)
    {        
        this.stats = stats;
        this.enemy = enemy;

        time = stats.buffDuration;
        countdown = time;
    }

    public void Update()
    {
        if(countdown <= 0f)
        {
            enemy.RemoveBuff(stats);
        }

        countdown -= Time.deltaTime;
    }
}
