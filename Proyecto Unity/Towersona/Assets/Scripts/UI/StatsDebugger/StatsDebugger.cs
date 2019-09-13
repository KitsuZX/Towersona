using UnityEngine;
using TMPro;

public class StatsDebugger : MonoBehaviour
{
	protected AttackPattern pattern;
	protected TextMeshProUGUI text;

	protected CameraController controller;
	protected TowersonaStats stats;

	protected void Awake()
	{
		pattern = GetComponentInParent<AttackPattern>();
		text = GetComponentInChildren<TextMeshProUGUI>();		
	}

	protected virtual void Update()
	{
		if (DebuggingOptions.Instance.showStats)
		{
			text.text = "Strength: " + pattern.AttackStrength.ToString("F2") + "\n" +
						"Att Speed: " + pattern.AttackSpeed.ToString("F2") + "\n" +
						"Range: " + pattern.currentAttackRange.ToString("F2") + "\n" +
						"Bul Speed: " + pattern.currentBulletSpeed.ToString("F2");
		}
		else
		{
			if(text.text != "")
			{
				text.text = "";
			}
		}
	}
}
