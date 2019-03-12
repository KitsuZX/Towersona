using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    protected GameObject bulletPrefab;
    [Header("Transform parameters")]
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private Transform[] partsToRotate;


    [Header("Attacking parameters -> Min, Max")]
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 attackStrength = new Vector2(0.5f, 10f);
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 attackSpeed = new Vector2(0.25f, 2f);
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 attackRange = new Vector2(1f, 5);
    [SerializeField][Tooltip("Min, Max")]
    private Vector2 bulletSpeed = new Vector2(2f, 10f);

    
    protected float currentAttackStrength;
    protected float currentAttackSpeed;
    protected float currentAttackRange;
    protected float currentBulletSpeed;

    [HideInInspector]
    public Transform target;

    protected TowersonaStats towersona;
    protected TowersonaLODAnimation animations;

    private float fireCountdown = 1f;   

    private void Awake()
    {
        towersona = GetComponent<TowersonaStats>();
        animations = GetComponent<TowersonaLODAnimation>();

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
        LockOnTarget(target);
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
        /*if (target == null)
        {
            animations.IdleAnimation();
            return;
        }


        animations.Shoot();*/
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

    /// <summary>
    /// Rotates the model to look to a given target
    /// </summary>
    public void LockOnTarget(Transform target)
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            for (int i = 0; i < partsToRotate.Length; i++)
            {
                Vector3 rotation = Quaternion.Lerp(partsToRotate[i].rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
                partsToRotate[i].rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }
    }


}

