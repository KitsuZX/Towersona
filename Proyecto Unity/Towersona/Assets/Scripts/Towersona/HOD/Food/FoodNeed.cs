using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNeed : MonoBehaviour
{
    public float CurrentLevel { get; protected set; }

    private float decayPerSecond = 0.05f;
    private float maxLevel = 1;


    public void Feed(float hungerFulfilment)
    {
        CurrentLevel = Mathf.Min(maxLevel, CurrentLevel + hungerFulfilment);
    }

    public void SetStats(TowersonaStats stats)
    {
        maxLevel = stats.maxFood;
        decayPerSecond = stats.foodDecayPerSecond;
    }

    public void ResetNeed()
    {
        CurrentLevel = maxLevel;
    }

    

    /// <summary>
    /// Does the need decay. Doesn't take towersona count into account.
    /// </summary>
    private void Update()
    {
        float decayThisStep = decayPerSecond * Time.deltaTime;
        CurrentLevel = Mathf.Max(0, CurrentLevel - decayThisStep);
    }

    

}
