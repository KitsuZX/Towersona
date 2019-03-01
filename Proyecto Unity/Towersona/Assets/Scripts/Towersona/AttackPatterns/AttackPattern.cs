using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{
    [Header("Max Attacking parameters")]
    [SerializeField]
    private float maxAttackStrength = 10f;
    [SerializeField]
    private float maxAttackSpeed = 2f;
    [SerializeField]
    private float maxAttackRange = 5f;
    [SerializeField]
    private float maxBulletSpeed = 10f;
    [Header("Min Attacking parameters")]
    [SerializeField]
    private float minAttackStrength = 0.5f;
    [SerializeField]
    private float minAttackSpeed = 0.25f;
    [SerializeField]
    private float minAttackRange = 1f;
    [SerializeField]
    private float minBulletSpeed = 2f;

    [Header("References")]
    public GameObject bulletPrefab;

    [HideInInspector]
    public float attackStrength;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float attackRange;
    [HideInInspector]
    public float bulletSpeed;

    [HideInInspector]
    public Transform target;

    protected Towersona towersona;
    private float fireCountdown = 1f;
    private TowersonaAnimations animations;


    private void Awake()
    {
        towersona = GetComponent<Towersona>();
        animations = GetComponent<TowersonaAnimations>();

        attackSpeed = maxAttackSpeed;
        attackStrength = maxAttackStrength;
        attackRange = maxAttackRange;
        attackSpeed = maxAttackSpeed;
    }

    private void Start()
    {
        InvokeRepeating("UpdateStats", 0f, 1f);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            animations.IdleAnimation();
            return;
        }


        animations.FightAnimation();

        towersona.LockOnTarget(target);

        if (fireCountdown <= 0f)
        {
            Shoot(target);
            fireCountdown = 1f / attackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void UpdateStats()
    {
        attackStrength = Mathf.Lerp(minAttackStrength, maxAttackStrength, towersona.towersonaNeeds.HappinessLevel);
        attackSpeed = Mathf.Lerp(minAttackSpeed, maxAttackSpeed, towersona.towersonaNeeds.HappinessLevel);
        attackRange = Mathf.Lerp(minAttackRange, maxAttackRange, towersona.towersonaNeeds.HappinessLevel);
        bulletSpeed = Mathf.Lerp(minBulletSpeed, maxBulletSpeed, towersona.towersonaNeeds.HappinessLevel);
    }

    public abstract void Shoot(Transform target);
    public abstract void UpdateTarget();

}
