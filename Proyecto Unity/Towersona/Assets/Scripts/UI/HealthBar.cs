using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Enemy enemy = null;	
	[SerializeField] private Canvas canvas = null;

	private EnemyStats stats;
	private Image healthBarImage;

	private void Awake()
	{
		healthBarImage = GetComponent<Image>();

		stats = enemy.enemyStats;
		healthBarImage.fillAmount = 1;
		canvas.enabled = false;
	}

	public void TakeDamage()
	{
		if (stats.lifes < stats.initialLifes && canvas.enabled == false)
		{
			canvas.enabled = true;
		}

		float amount = stats.lifes / stats.initialLifes;
		healthBarImage.fillAmount = amount;
	}
}
