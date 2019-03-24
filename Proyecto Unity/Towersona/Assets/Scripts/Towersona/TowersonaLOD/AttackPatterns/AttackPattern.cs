using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{ 
    [Header("References")][SerializeField]
    protected GameObject bulletPrefab;
    
    public float currentAttackStrength;
    public float currentAttackSpeed;
    public float currentAttackRange;
    public float currentBulletSpeed;

    [HideInInspector]
    public Transform target;

    protected TowersonaLOD towersonaLOD;
    protected TowersonaLODAnimation animations;

    private TowersonaNeeds needs;
    private TowersonaStats stats;

    private float fireCountdown = 1f;   

    private void Awake()
    {    
        towersonaLOD = GetComponent<TowersonaLOD>();
        animations = GetComponent<TowersonaLODAnimation>();        
    }

    private void Start()
    {
        stats = towersonaLOD.towersona.stats;
        needs = towersonaLOD.towersona.towersonaNeeds;

        currentAttackStrength = stats.attackStrength.y;
        currentAttackSpeed = stats.attackSpeed.y;
        currentAttackRange = stats.attackRange.y;
        currentBulletSpeed = stats.bulletSpeed.y;

        InvokeRepeating("UpdateStats", 0f, 1f);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (needs == null || stats == null) return; 

        CheckAnimations();
        towersonaLOD.LockOnTarget(target);
        CheckIfShouldShoot();      
    }

    private void UpdateStats()
    {
        currentAttackStrength = Mathf.Lerp(stats.attackStrength.x, stats.attackStrength.y, needs.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(stats.attackSpeed.x, stats.attackSpeed.y, needs.HappinessLevel);
        currentAttackRange = Mathf.Lerp(stats.attackRange.x, stats.attackRange.y, needs.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(stats.bulletSpeed.x, stats.bulletSpeed.y, needs.HappinessLevel);
    }

    private void OnDrawGizmos()
    {
        /*if (EditorApplication.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(towersona.transform.position, stats.attackRange.y);
        }*/
    }

    private void CheckAnimations()
    {
        if (target == null)
        {
            animations.IdleAnimation();
            return;
        }


        animations.Shoot();
    }

    private void CheckIfShouldShoot()
    {
        if (fireCountdown <= 0f && target != null)
        {
            Shoot(target);
            fireCountdown = 1f / currentAttackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

    public abstract void Shoot(Transform target);
    public abstract void UpdateTarget();  

}

