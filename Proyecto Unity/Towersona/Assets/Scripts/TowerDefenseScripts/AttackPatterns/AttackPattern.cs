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
    public Transform firePoint;

    [HideInInspector]
    public float attackStrength;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float attackRange;
    [HideInInspector]
    public float bulletSpeed;

    private Towersona towersona;

    private void Awake()
    {
        towersona = GetComponent<Towersona>();

        attackSpeed = maxAttackSpeed;
        attackStrength = maxAttackStrength;
        attackRange = maxAttackRange;
        attackSpeed = maxAttackSpeed;
    }

    private void Start()
    {
        InvokeRepeating("UpdateStats", 0f, 1f);
    }


    private void UpdateStats()
    {
        attackStrength = Mathf.Lerp(minAttackStrength, maxAttackStrength, towersona.towersonaNeeds.HappinessLevel);
        attackSpeed = Mathf.Lerp(minAttackSpeed, maxAttackSpeed, towersona.towersonaNeeds.HappinessLevel);
        attackRange = Mathf.Lerp(minAttackRange, maxAttackRange, towersona.towersonaNeeds.HappinessLevel);
        bulletSpeed = Mathf.Lerp(minBulletSpeed, maxAttackSpeed, towersona.towersonaNeeds.HappinessLevel);
    }

    public abstract void Shoot(Transform target);    
}
