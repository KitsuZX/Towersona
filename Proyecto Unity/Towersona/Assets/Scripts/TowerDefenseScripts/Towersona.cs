using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towersona : MonoBehaviour
{
    [SerializeField]
    [Header("References")]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firePoint;

    [SerializeField][Header("Attack parameters")]
    private float attackStrength = 3;
    [SerializeField]
    private float attackSpeed = 2f;
    [SerializeField]
    private float attackRange = 5f;

    [Header("Transform parameters")]
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private Transform[] partsToRotate;

    private Transform target;
    private Enemy targetEnemy;
    private float fireCountdown = 0f;

    private TowersonaNeeds towersonaNeeds;
    private Color color;
    
    public Camera towersonaNeedsCamera;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Awake()
    {
        color = Random.ColorHSV();
        GetComponentInChildren<MeshRenderer>().material.color = color;
        towersonaNeeds = World.Instance.SpawnDetailedTowersonaView(color, this);
        towersonaNeedsCamera = towersonaNeeds.transform.parent.GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        attackRange *= towersonaNeeds.HappinessLevel;
        attackSpeed *= towersonaNeeds.HappinessLevel;
        attackStrength *= towersonaNeeds.HappinessLevel;

        if (target == null)
        {
            return;
        }

        LockOnTarget();

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / attackSpeed;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(-dir);
        for (int i = 0; i < partsToRotate.Length; i++)
        {
            Vector3 rotation = Quaternion.Lerp(partsToRotate[i].rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partsToRotate[i].rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.damage = attackStrength;

        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnMouseUpAsButton()
    {
        World.Instance.ChangeCamera(this);
    }
}
