using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn
{
	public float amount;
	public float burnTime;

	private Enemy enemy;

	public Burn(float amount, float time, Enemy enemy)
	{
		this.amount = amount;
		this.burnTime = time;
		this.enemy = enemy;
	}

	public void Update()
	{
		if (burnTime != Mathf.Infinity)
		{
			burnTime -= Time.deltaTime;
			if (burnTime < 0)
			{
				RemoveBurn();
			}
		}

		if (enemy == null)
		{
			RemoveBurn();
		}
	}

	private void RemoveBurn()
	{
		enemy.RemoveBurn(this);
	}
}
