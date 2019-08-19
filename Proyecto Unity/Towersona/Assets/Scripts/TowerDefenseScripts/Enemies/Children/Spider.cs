using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField]
    private EnemyStats enemyStats;
    
    private void Awake()
    {
        stats = enemyStats;
    }
    private void Update()
    {
		base.Update();
    }
}
