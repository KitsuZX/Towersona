using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : Enemy
{    
    /*private BufferStats bufferStats;

    private float countDown = 0f;

    private void Start()
    {
		bufferStats = (BufferStats)stats;
        countDown = bufferStats.timeBetweenBuffs;
    }

    void Update()
    {
        base.Update();

        if(countDown <= 0)
        {
            //Lanzar buff
            countDown = bufferStats.timeBetweenBuffs;
        }

        countDown -= Time.deltaTime;
    }

    void BuffEnemies()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, bufferStats.range);

        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().Buff(bufferStats);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
		if (Application.isPlaying)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, bufferStats.range);
		}
    }*/
}
