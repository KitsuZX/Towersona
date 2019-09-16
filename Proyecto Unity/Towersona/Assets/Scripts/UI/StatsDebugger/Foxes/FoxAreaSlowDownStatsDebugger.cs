using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxAreaSlowDownStatsDebugger : StatsDebugger
{
	FoxSlowDownAreaAttack foxAttack;

	private void Start()
	{
		foxAttack = (FoxSlowDownAreaAttack)pattern;
	}

	protected override void Update()
	{
		if (DebuggingOptions.Instance.showStats)
		{
			text.text = "Strength: " + foxAttack.AttackStrength.ToString("F2") + "\n" +
						"Att Speed: " + foxAttack.AttackSpeed.ToString("F2") + "\n" +
						"Range: " + foxAttack.currentAttackRange.ToString("F2") + "\n" +
						"Bul Speed: " + foxAttack.currentBulletSpeed.ToString("F2") + "\n" +
						"SD percent: " + foxAttack.currentSlowDownPercentage.ToString("F2");

		}
		else
		{
			if (text.text != "")
			{
				text.text = "";
			}
		}
	}
}
