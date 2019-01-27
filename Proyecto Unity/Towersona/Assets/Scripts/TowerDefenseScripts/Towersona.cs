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
    [SerializeField]
    private GameObject notificationPrefab;
    [SerializeField]
    private Animator headAC;
    [SerializeField]
    private Animator bodyAC;

    [SerializeField][Header("Attack parameters")]
    private float attackStrength = 3;
    [SerializeField]
    private float attackSpeed = 2f;
    [SerializeField]
    private float attackRange = 5f;
    [SerializeField]
    private float bulletSpeed = 10f;
    [SerializeField]  
    private float minAttackStrength = 0.5f;
    [SerializeField]
    private float minAttackSpeed = 0.25f;
    [SerializeField]
    private float minAttackRange = 1f;
    [SerializeField]
    private float minBulletSpeed = 2f;

    [Header("Transform parameters")]
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private Transform[] partsToRotate;    
    
    [HideInInspector]
    public Tile tile;


    [HideInInspector]
    public bool isAttacking = false;
    [HideInInspector]
    public Camera towersonaNeedsCamera;

    private Transform target;
    private Enemy targetEnemy;
    private float fireCountdown = 0f;
 

    private TowersonaNeeds towersonaNeeds;
    private TowersonaAnimation detailedAnimationManager;
    private Color color;
    private bool isNotifying;
    private GameObject notification;
    private TowersonaNeeds.NeedType prevNeedType;
    
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
        detailedAnimationManager = towersonaNeeds.GetComponent<TowersonaAnimation>();
        towersonaNeedsCamera = towersonaNeeds.transform.parent.GetComponentInChildren<Camera>();

        World.Instance.ChangeCamera(this);
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
        bulletSpeed *= towersonaNeeds.HappinessLevel;

        attackRange = Mathf.Max(attackRange, minAttackRange);
        attackSpeed = Mathf.Max(attackSpeed, minAttackSpeed);
        attackStrength = Mathf.Max(attackStrength, minAttackStrength);
        bulletSpeed = Mathf.Max(bulletSpeed, minBulletSpeed);

        if (target == null)
        {
            isAttacking = false;
            detailedAnimationManager.isFighting = false;
            headAC.SetBool("isFighting", false);
            bodyAC.SetBool("isFighting", false);
            return;
        }

        isAttacking = true;
        detailedAnimationManager.isFighting = true;
        headAC.SetBool("isFighting", true);
        bodyAC.SetBool("isFighting", true);

        LockOnTarget();

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / attackSpeed;
        }

        fireCountdown -= Time.deltaTime;

        //Check for needs
        TowersonaNeeds.NeedType needType = towersonaNeeds.CheckIfShouldNotifyNeed();

        if (needType != TowersonaNeeds.NeedType.None && !isNotifying)
        {
            CreateNotification(needType);
        }

        if (isNotifying)
        {
            if (prevNeedType != needType)
            {
                DestroyNotification();

                if(needType != TowersonaNeeds.NeedType.None)
                {
                    CreateNotification(needType);
                }
            }
        }
       

        prevNeedType = needType;
    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
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
        bullet.speed = bulletSpeed;

        if (bullet != null)
            bullet.Seek(target);
    }

    private void CreateNotification(TowersonaNeeds.NeedType needType)
    {
        isNotifying = true;

        Notification notification = Instantiate(notificationPrefab).GetComponent<Notification>();

        this.notification = notification.gameObject;

        Vector3 position = transform.position;
        position.x += 1f;
        position.y = 2f;
        position.z += 1f;

        notification.transform.position = position;

        notification.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        SpriteRenderer[] sr = notification.GetComponentsInChildren<SpriteRenderer>();
        notification.transform.SetParent(transform);

        SpriteRenderer spriteRenderer = new SpriteRenderer();

        foreach (SpriteRenderer s in sr)
        {
            if(s != GetComponent<SpriteRenderer>())
            {
                spriteRenderer = s;
            }           
        }

        switch (needType)
        {
            case TowersonaNeeds.NeedType.Hunger:
                spriteRenderer.sprite = notification.hungerSprite;
                break;
            case TowersonaNeeds.NeedType.Love:
                spriteRenderer.sprite = notification.noLoveSprite;
                break;
            case TowersonaNeeds.NeedType.Shit:
                spriteRenderer.sprite = notification.shitSprite;
                break;
        }
    }

    private void DestroyNotification()
    {
        isNotifying = false;
        Destroy(notification);
    }

    private void OnMouseUpAsButton()
    {
        World.Instance.ChangeCamera(this);
        TowerDefenseManager.Instance.SelectTile(tile);
    }
}
