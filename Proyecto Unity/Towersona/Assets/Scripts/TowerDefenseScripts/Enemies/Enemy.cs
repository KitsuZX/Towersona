using BezierSolution;
using UnityEngine;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
	public float Speed {
		get
		{
			return baseSpeed - slowDownAmount;
		}
	}

    [SerializeField]
    protected float baseSpeed = 2f;
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
	[HideInInspector]
	public float slowDownCountDown;


	protected Transform target;
    private int controlPointIndex = 0;

	private bool isSlowedDown;
	private float slowDownAmount = 0;
	private List<SlowDownType> slowDowns;

	private void Awake()
	{
		slowDowns = new List<SlowDownType>();
	}

	protected void Update()
	{
		slowDownAmount -= Time.deltaTime;
		if (slowDownAmount < 0)
		{
			RemoveSlowDown();
		}
	}    
    

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

        LevelManager.Instance.enemiesAlive--;
    }

    public void TakeDamage(float amount) {
        life -= amount;
        if(life <= 0)
        {
            KillEnemy();
        }
    }

	public void AddSlowDown(float amount, float time, SlowDownType type)
	{
		if (!slowDowns.Contains(type))
		{
			slowDowns.Add(type);
			if (!isSlowedDown) isSlowedDown = true;
			slowDownAmount += amount;
			GetComponent<BezierWalkerWithSpeed>().speed = Speed;
		}

		if(time > slowDownCountDown)
		{
			slowDownCountDown = time;
		}
	}

	private void RemoveSlowDown()
	{
		slowDowns.Clear();
		slowDownAmount = 0;
		GetComponent<BezierWalkerWithSpeed>().speed = Speed;
		isSlowedDown = false;
	}

	public enum SlowDownType { Fox, Penguin }
}
