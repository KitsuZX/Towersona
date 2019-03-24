using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float speed = 2f;
    [SerializeField]
    protected float life = 30f;
    [SerializeField][Tooltip("Number of lifes the player loses if this enemy reaches the end")]
    protected int damage = 1;
    [SerializeField]
    protected int value = 20;
    [SerializeField]
    private GameObject deathEffect;


    protected Transform target;
    private int controlPointIndex = 0;

    //Private references
    private GameManager gameManager;
    private World world;

    private void Awake()
    {
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        target = world.controlPoints[0].transform;        
    }

    protected abstract void Update();   
 
    protected void GetNextWaypoint()
    {
        if(controlPointIndex >= world.controlPoints.Count - 1)
        {
            EndPath();
            return;
        }

        controlPointIndex++;
        target = world.controlPoints[controlPointIndex];
        transform.LookAt(target);
    }

    private void EndPath()
    {
        KillEnemy(true);
        PlayerStats.Instance.LoseLive(damage);
        
    }

    private void KillEnemy(bool endPath = false)
    {
        CameraShake.Instance.AddTrauma(0.4f);
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        if (!endPath)
        {            
            BuildManager.Instance.SpawnEffect(deathEffect, pos);
            PlayerStats.Instance.AddMoney(value);
        }

        Destroy(gameObject);

        gameManager.enemiesAlive--;
    }

    public void TakeDamage(float amount) {
        life -= amount;
        if(life <= 0)
        {
            KillEnemy();
        }
    }


}
