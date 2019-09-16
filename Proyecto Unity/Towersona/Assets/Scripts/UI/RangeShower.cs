using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShower : MonoBehaviour
{
    [SerializeField] private GameObject minRange;
    [SerializeField] private GameObject maxRange;

    public void ShowRange(Vector3 position, TowersonaStats stats)
    {
        minRange.SetActive(true);
        maxRange.SetActive(true);

        minRange.transform.localScale = new Vector3(stats.range.x * 2f, stats.range.x * 2f, stats.range.x * 2f);
        maxRange.transform.localScale = new Vector3(stats.range.y * 2f, stats.range.y * 2f, stats.range.y * 2f);

        minRange.transform.position = position;
        maxRange.transform.position = position;
    }

    public void HideRange()
    {
        minRange.SetActive(false);
        maxRange.SetActive(false);
    }
}
