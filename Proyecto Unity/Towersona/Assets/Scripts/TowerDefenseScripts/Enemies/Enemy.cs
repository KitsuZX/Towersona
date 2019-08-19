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
				return stats.speed;
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

			return stats.speed * (1 - totalAmount);
		}
	}
    public bool Flies {
        get
        {
            return stats.flies;
        }
    }

	private Color burnColor = new Color(215, 105, 0, 196);

    protected EnemyStats stats;

    [SerializeField]
    private GameObject deathEffect;

	private Dictionary<SlowDownType, List<SlowDown>> slowDowns = new Dictionary<SlowDownType, List<SlowDown>>();
	private BezierWalkerWithSpeed bezierWalkerWithSpeed;

	private List<Burn> burns = new List<Burn>();

	private List<GameObject> slowDownSources = new List<GameObject>();
	private List<GameObject> burnSources = new List<GameObject>();

	private MeshRenderer[] meshRenderers;

	private void Start()
	{
		meshRenderers = GetComponentsInChildren<MeshRenderer>();

		InvokeRepeating("TakeBurnDamage", 0f, 0.5f);

        stats.Initialize();
	}

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

		foreach (Burn burn in burns.ToList())
		{
			burn.Update();
		}
	}        

    public void EndPath()
    {
        KillEnemy(true);
        PlayerStats.Instance.LoseLive(stats.damage);
        
    }

    private void KillEnemy(bool endPath = false)
    {      
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        if (!endPath)
        {            
            BuildManager.Instance.SpawnEffect(deathEffect, pos);
            PlayerStats.Instance.AddMoney(stats.value);
        }

        Destroy(gameObject);

        LevelManager.Instance.enemiesAlive--;
    }

    public void TakeDamage(float amount) {
        stats.lifes -= amount;
        if(stats.lifes <= 0)
        {
            KillEnemy();
        }
    }


	#region SlowDowns
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

		slowDownSources.Add(source);

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
		return slowDownSources.Contains(source);
	}
	#endregion

	#region Burn
	public void SetOnFire(float amount, float burnTime, GameObject source)
	{
		Burn b = new Burn(amount, burnTime, this);

		burnSources.Add(source);

		burns.Add(b);

		Tint(burnColor);
	}

	private void TakeBurnDamage()
	{
		if(burns.Count > 0)
		{
			Tint(new Color(0.2f, 0, 0));
			Invoke("Tint", 0.1f);
			float[] values = new float[burns.Count];

			for (int i = 0; i < burns.Count; i++)
			{
				values[i] = burns[i].amount;
			}

			float burn = values.Max();
			float time = 0;

			for (int i = 0; i < burns.Count; i++)
			{
				if(burns[i].amount == burn)
				{
					time = burns[i].burnTime;
				}
			}

			float damage = burn / (time * 0.5f);
			TakeDamage(damage);


			Tint(Color.red);
		}
	}

	private void Tint(Color color)
	{
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			foreach(Material m in meshRenderer.materials)
			{
				m.SetColor("_Color", color);
			}			
		}
	}

	private void Tint()
	{
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			foreach (Material m in meshRenderer.materials)
			{
				m.SetColor("_Color", burnColor);
			}
		}
	}

	private void RemoveTint()
	{
		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			foreach (Material m in meshRenderer.materials)
			{
				m.SetColor("_Color", Color.clear);
			}
		}
	}

	public bool AlredyBurnedByTowersona(GameObject source)
	{
		return burnSources.Contains(source);
	}

	public void RemoveBurn(Burn burn)
	{
		burns.Remove(burn);

		if(burns.Count == 0)
		{
			RemoveTint();
		}
	}

    #endregion

    #region Buffs
    public void Buff(BufferStats bufferStats)
    {
        Buff buff = new Buff(bufferStats, this);
    }

    public void RemoveBuff(BufferStats bufferStats)
    {

    }
    #endregion

    public enum SlowDownType { Fox, Area }

}
