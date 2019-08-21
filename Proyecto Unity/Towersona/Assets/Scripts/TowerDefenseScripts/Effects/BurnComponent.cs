using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnComponent : MonoBehaviour
{
	public float burnDamage;

	private float time;	
	private float interval = 0.5f;

	private Enemy enemy;
	private Color burnColor1;
	private Color burnColor2;

	public void StartBurningEnemy(float burnDamage, Enemy enemy, Color burnColor1, Color burnColor2)
	{		
		this.enemy = enemy;
		this.burnDamage = burnDamage;
		this.burnColor1 = burnColor1;
		this.burnColor2 = burnColor2;

		InvokeRepeating("BurnEnemy", 0f, interval);
	}

	public void StopBurningEnemy()
	{
		enemy.RemoveTint();
		CancelInvoke("BurnEnemy");
		Destroy(this);
	}

	private void BurnEnemy()
	{
		enemy.Tint(burnColor2);
		Invoke("TintEnemy", 0.1f);

		float damage = burnDamage * interval;
		enemy.TakeDamage(damage);
	}

	private void TintEnemy()
	{
		enemy.Tint(burnColor1);
	}
}
