using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{
    [Header("Attacking parameters -> Min, Max")]
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 attackStrength = new Vector2(0.5f, 10f);
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 attackSpeed = new Vector2(0.25f, 2f);
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 attackRange = new Vector2(1f, 5);
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 bulletSpeed = new Vector2(2f, 10f);

    [Header("References")][SerializeField]
    protected GameObject bulletPrefab;
    
    protected float currentAttackStrength;
    protected float currentAttackSpeed;
    protected float currentAttackRange;
    protected float currentBulletSpeed;

    [HideInInspector]
    public Transform target;

    protected Towersona towersona;
    protected TowersonaAnimations animations;

    private float fireCountdown = 1f;   

    private void Awake()
    {
        towersona = GetComponent<Towersona>();
        animations = GetComponent<TowersonaAnimations>();

        currentAttackStrength = attackStrength.y;
        currentAttackSpeed = attackSpeed.y;      
        currentAttackRange = attackRange.y;
        currentBulletSpeed = bulletSpeed.y;

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
        currentAttackStrength = Mathf.Lerp(attackStrength.x, attackStrength.y, towersona.towersonaNeeds.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, towersona.towersonaNeeds.HappinessLevel);
        currentAttackRange = Mathf.Lerp(attackRange.x, attackRange.y, towersona.towersonaNeeds.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, towersona.towersonaNeeds.HappinessLevel);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(towersona.transform.position, currentAttackRange);
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

