using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAreaAttackDebugger : StatsDebugger
{
	DragonAttack dragonAttack;

	private void Start()
	{
		dragonAttack = (DragonAttack)pattern;
	}

	protected override void Update()
	{
		if (DebuggingOptions.Instance.showStats)
		{
			text.text = "Strength: " + dragonAttack.AttackStrength.ToString("F2") + "\n" +
						"Att Speed: " + dragonAttack.AttackSpeed.ToString("F2") + "\n" +
						"Range: " + dragonAttack.currentAttackRange.ToString("F2") + "\n" +
						"Bul Speed: " + dragonAttack.currentBulletSpeed.ToString("F2") + "\n" +
						"Area Size: " + dragonAttack.currentDamageArea.ToString("F2") + "\n" +
						"Damage Red: " + dragonAttack.currentAreaDamageReduction.ToString("F2") + "\n" +
						"SD Percent: " + dragonAttack.currentSlowDownPercentage.ToString("F2") + "\n" +
						"SD Time: " + dragonAttack.currentSlowDownTime.ToString("F2") + "\n" +
						"Area Life: " + dragonAttack.currentSlowDownAreaLifeTime.ToString("F2");
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
