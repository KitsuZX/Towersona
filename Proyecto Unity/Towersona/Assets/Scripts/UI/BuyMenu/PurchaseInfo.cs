using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseInfo : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI nameText = null;
	[SerializeField] private TextMeshProUGUI description = null;
	[SerializeField] private TextMeshProUGUI attackText = null;
	[SerializeField] private TextMeshProUGUI attackSpeedText = null;

	public void SetInfo(Towersona towersona, int upgradeIndex)
	{
		int index = upgradeIndex + 1;
		nameText.text = towersona.names[index];
		this.description.text = towersona.descriptions[index];

		TowersonaStats stats = towersona.statsArray[index];
		attackText.text = stats.bulletDamage.x + " - " + stats.bulletDamage.y;
		attackSpeedText.text = stats.attackSpeed.x + " - " + stats.attackSpeed.y;
	}
}
