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
    private float maxAttackStrength = 10f;
    [SerializeField]
    private float maxAttackSpeed = 2f;
    [SerializeField]
    private float maxAttackRange = 5f;
    [SerializeField]
    private float maxBulletSpeed = 10f;
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
    [HideInInspector]
    public Color color;

    private float attackStrength;
    private float attackSpeed;
    private float attackRange;
    private float bulletSpeed;
    private Transform target;
    private Enemy targetEnemy;
    private float fireCountdown = 0f;
    private TowersonaNeeds towersonaNeeds;
    private TowersonaAnimation detailedAnimationManager;
    private bool isNotifying;
    private GameObject notification;
    private TowersonaNeeds.NeedType prevNeedType;
    private TowersController towersController;
    private World world;
    private GameManager gameManager;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Awake()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        towersController = gm.GetComponent<TowersController>();
        gameManager = gm.GetComponent<GameManager>();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        color = towersController.GetColor();
        towersonaNeeds = towersController.SpawnDetailedTowersonaView(this);

        detailedAnimationManager = towersonaNeeds.GetComponent<TowersonaAnimation>();
        towersonaNeedsCamera = towersonaNeeds.transform.parent.GetComponentInChildren<Camera>();
      
        attackSpeed = maxAttackSpeed;
        attackStrength = maxAttackStrength;
        attackRange = maxAttackRange;
        attackSpeed = maxAttackSpeed; 

      
       gameManager.ChangeCamera(this);

       GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("UpdateStats", 0f, 1f);
    }

    public void ChangeColor()
    {
        MeshRenderer[] mr = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer m in mr)
        {
            m.material.color = color;
        }

        SkinnedMeshRenderer[] smr = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer m in smr)
        {
            m.material.color = color;
        }
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

    private void UpdateStats()
    {      
        attackStrength = Mathf.Lerp(minAttackStrength, maxAttackStrength, towersonaNeeds.HappinessLevel);
        attackSpeed = Mathf.Lerp(minAttackSpeed, maxAttackSpeed, towersonaNeeds.HappinessLevel);
        attackRange = Mathf.Lerp(minAttackRange, maxAttackRange, towersonaNeeds.HappinessLevel);
        bulletSpeed = Mathf.Lerp(minBulletSpeed, maxAttackSpeed, towersonaNeeds.HappinessLevel);

    }

    private void Update()
    {       
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
        gameManager.ChangeCamera(this);
        world.SelectTile(tile);
    }
}
