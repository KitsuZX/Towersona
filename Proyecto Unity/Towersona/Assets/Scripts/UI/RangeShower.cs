using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShower : MonoBehaviour
{
    [SerializeField] private GameObject minRange = null;
    [SerializeField] private GameObject maxRange = null;

    public void ShowRange(Vector3 position, TowersonaStats stats)
    {
        minRange.SetActive(true);
        maxRange.SetActive(true);

        minRange.transform.localScale = new Vector3(stats.range.x, stats.range.x, stats.range.x);
        maxRange.transform.localScale = new Vector3(stats.range.y, stats.range.y, stats.range.y);

        minRange.transform.position = position;
        maxRange.transform.position = position;
    }
	 
    public void HideRange()
    {
        minRange.SetActive(false);
        maxRange.SetActive(false);
    }
}
