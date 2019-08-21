using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{ 
    [HideInInspector]
    public Transform target;
    private Enemy enemyTarget;

    protected TowersonaLOD towersonaLOD;
    protected TowersonaLODAnimation animations;

    private TowersonaNeeds needs;
    protected TowersonaStats stats;

    private float fireCountdown = 1f;   

    protected virtual void Awake()
    {    
        towersonaLOD = GetComponent<TowersonaLOD>();
        animations = GetComponent<TowersonaLODAnimation>();        
    }

    protected virtual void Start()
    {
        stats = towersonaLOD.towersona.stats;
        needs = towersonaLOD.towersona.towersonaNeeds;     

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

    private void UpdateStats()
    {
        stats.UpdateStats();
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
                    fireCountdown = 1f / stats.currentAttackSpeed;
                }
            }
            else
            {
                Shoot(target);
                fireCountdown = 1f / stats.currentAttackSpeed;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    public abstract void Shoot(Transform target);
    public abstract void UpdateTarget();  

}

