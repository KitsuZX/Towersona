using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{ 
    [Header("References")][SerializeField]
    protected GameObject bulletPrefab;  
  
    [HideInInspector]
    public Transform target;

    protected TowersonaLOD towersonaLOD;
    protected TowersonaLODAnimation animations;

    private TowersonaNeeds needs;
    protected TowersonaStats stats;

    private float fireCountdown = 1f;   

    protected void Awake()
    {    
        towersonaLOD = GetComponent<TowersonaLOD>();
        animations = GetComponent<TowersonaLODAnimation>();        
    }

    public void Start()
    {
        stats = towersonaLOD.towersona.stats;
        needs = towersonaLOD.towersona.towersonaNeeds;     

        InvokeRepeating("UpdateStats", 0f, 1f);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    protected void Update()
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

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(towersonaLOD.transform.position, stats.currentAttackRange);
        }
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
            Shoot(target);
            fireCountdown = 1f / stats.currentAttackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

    public abstract void Shoot(Transform target);
    public abstract void UpdateTarget();  

}

