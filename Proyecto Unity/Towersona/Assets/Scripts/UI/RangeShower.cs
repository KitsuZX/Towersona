using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShower : MonoBehaviour
{
    [SerializeField] private GameObject minRange = null;
    [SerializeField] private GameObject maxRange = null;
	[SerializeField] private GameObject currentRange = null;

	AttackPattern pattern;

	public void ShowCurrentMaxMinRange(Vector3 position, TowersonaStats stats)
    {
        minRange.SetActive(true);
        maxRange.SetActive(true);

        minRange.transform.localScale = new Vector3(stats.range.x, stats.range.x, stats.range.x);
        maxRange.transform.localScale = new Vector3(stats.range.y, stats.range.y, stats.range.y);

        minRange.transform.position = position;
        maxRange.transform.position = position;
    }
	 
    public void HideMinMaxRange()
    {
        minRange.SetActive(false);
        maxRange.SetActive(false);	
    }

	public void ShowCurrentRange(Vector3 position, AttackPattern pattern)
	{
		this.pattern = pattern;
		currentRange.SetActive(true);
		currentRange.transform.localScale = new Vector3(pattern.currentAttackRange, pattern.currentAttackRange, pattern.currentAttackRange);
		currentRange.transform.position = position;
	}

	public void HideCurrentRange()
	{
		currentRange.SetActive(false);
	}

	private void Update()
	{
		if (currentRange.activeSelf)
		{
			currentRange.transform.localScale = new Vector3(pattern.currentAttackRange, pattern.currentAttackRange, pattern.currentAttackRange);
		}
	}
}
