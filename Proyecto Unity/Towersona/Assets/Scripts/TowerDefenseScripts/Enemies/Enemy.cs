using BezierSolution;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour
{
	public float Speed {
		get
		{
			if(slowDowns.Count == 0)
			{
				return baseSpeed;
			}

			float totalAmount = 0;		

			foreach (KeyValuePair<SlowDownType, List<SlowDown>> entry in slowDowns)
			{
				float[] slows = new float[entry.Value.Count];

				for (int i = 0; i < entry.Value.Count; i++)
				{
					slows[i] = entry.Value[i].amount;
				}

				float slowDownAmount = slows.Max();

				totalAmount += (1 - totalAmount) * slowDownAmount;
			}

			return baseSpeed * (1 - totalAmount);
		}
	}

    [SerializeField]
    protected float baseSpeed = 2f;
    [SerializeField]
    protected float life = 30f;
    [SerializeField][Tooltip("Number of lifes the player loses if this enemy reaches the end")]
    protected int damage = 1;
    [SerializeField][Tooltip("Dinero que da al morir")]
    protected int value = 20;

    [SerializeField]
    private GameObject deathEffect;

	private Dictionary<SlowDownType, List<SlowDown>> slowDowns= new Dictionary<SlowDownType, List<SlowDown>>();
	private BezierWalkerWithSpeed bezierWalkerWithSpeed;

	private List<GameObject> sources = new List<GameObject>();

	protected void Update()
	{
		if (!bezierWalkerWithSpeed)
		{
			bezierWalkerWithSpeed = GetComponent<BezierWalkerWithSpeed>();
		}
		else
		{
			bezierWalkerWithSpeed.speed = Speed;
		}

		//Update slowDowns
		foreach (KeyValuePair<SlowDownType, List<SlowDown>> entry in slowDowns.ToList())
		{
			for (int i = 0; i < entry.Value.Count; i++)
			{
				entry.Value[i].Update();
			}			
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

	public SlowDown AddSlowDown(float amount, float time, SlowDownType type, GameObject source)
	{
		SlowDown slowDown = new SlowDown(amount, time, type, this);

		if (!slowDowns.ContainsKey(type))
		{
			List<SlowDown> sd = new List<SlowDown>();
			slowDowns.Add(type, sd);
			sd.Add(slowDown);
		}
		else
		{
			slowDowns[type].Add(slowDown);
		}

		sources.Add(source);

		return slowDown;
	}

	public void RemoveSlowDown(SlowDown slowDown)
	{
		slowDowns[slowDown.type].Remove(slowDown);

		if(slowDowns[slowDown.type].Count == 0)
		{
			slowDowns.Remove(slowDown.type);
		}
	}

	public bool AlredySlownDownByTowersona(GameObject source)
	{
		return sources.Contains(source);
	}

	public enum SlowDownType { Fox }

}
