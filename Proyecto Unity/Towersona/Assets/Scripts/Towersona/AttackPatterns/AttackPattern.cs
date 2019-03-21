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

    protected Towersona towersona;
    protected TowersonaLODAnimation animations;

    private float fireCountdown = 1f;   

    private void Awake()
    {    
        towersona = GetComponent<Towersona>();
        animations = GetComponent<TowersonaLODAnimation>();

        currentAttackStrength = towersona.stats.attackStrength.y;
        currentAttackSpeed = towersona.stats.attackSpeed.y;      
        currentAttackRange = towersona.stats.attackRange.y;
        currentBulletSpeed = towersona.stats.bulletSpeed.y;

        InvokeRepeating("UpdateStats", 0f, 1f);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        CheckAnimations();
        towersona.LockOnTarget(target);
        CheckIfShouldShoot();      
    }

    private void UpdateStats()
    {
        currentAttackStrength = Mathf.Lerp(towersona.stats.attackStrength.x, towersona.stats.attackStrength.y, towersona.towersonaNeeds.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(towersona.stats.attackSpeed.x, towersona.stats.attackSpeed.y, towersona.towersonaNeeds.HappinessLevel);
        currentAttackRange = Mathf.Lerp(towersona.stats.attackRange.x, towersona.stats.attackRange.y, towersona.towersonaNeeds.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(towersona.stats.bulletSpeed.x, towersona.stats.bulletSpeed.y, towersona.towersonaNeeds.HappinessLevel);
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
        if (fireCountdown <= 0f)
        {
            Shoot(target);
            fireCountdown = 1f / currentAttackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

    public abstract void Shoot(Transform target);
    public abstract void UpdateTarget();  

}

