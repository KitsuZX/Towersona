using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerStatsDebugger : StatsDebugger
{
	CatAttack catAttack;

	private void Start()
	{
		catAttack = (CatAttack)pattern;
	}

	protected override void Update()
	{
		if (DebuggingOptions.Instance.showStats)
		{
			text.text = "Mon time: " + catAttack.currentTimeSpan.ToString("F2") + "\n" +
						"Mon amount: " + catAttack.currentMoneyGiven.ToString("F2") + "\n" +
						"Range: " + catAttack.currentBoostRange.ToString("F2") + "\n" +						
						"Strength +: " + catAttack.currentAttackStrengthBoost.ToString("F2") + "\n" +
						"Speed +: " + catAttack.currentAttackSpeedBoost.ToString("F2");
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
