using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown
{
	public float amount;
	public float time;
	public Enemy.SlowDownType type;

	private Enemy enemy;

	public SlowDown(float amount, float time, Enemy.SlowDownType type, Enemy enemy)
	{
		this.amount = amount;
		this.time = time;
		this.type = type;
		this.enemy = enemy;
	}

	public void Update()
	{
		if (time != Mathf.Infinity)
		{
			time -= Time.deltaTime;
			if (time < 0)
			{
				RemoveSlowDown();
			}
		}

		if (enemy == null)
		{
			RemoveSlowDown();
		}
	}

	private void RemoveSlowDown()
	{
		enemy.RemoveSlowDown(this);
	}
}
