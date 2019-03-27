using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

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

    [HideInInspector]
    public BezierSpline path;


    protected Transform target;
    private int controlPointIndex = 0; 
  

    protected abstract void Update();    
    

    public void EndPath()
    {
        KillEnemy(true);
        PlayerStats.Instance.LoseLive(damage);
        
    }

    private void KillEnemy(bool endPath = false)
    {      
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        if (!endPath)
        {            
            BuildManager.Instance.SpawnEffect(deathEffect, pos);
            PlayerStats.Instance.AddMoney(value);
        }

        Destroy(gameObject);

        GameManager.Instance.enemiesAlive--;
    }

    public void TakeDamage(float amount) {
        life -= amount;
        if(life <= 0)
        {
            KillEnemy();
        }
    }


}
