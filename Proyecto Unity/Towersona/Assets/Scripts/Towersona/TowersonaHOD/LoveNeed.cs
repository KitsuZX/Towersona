using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveNeed : MonoBehaviour
{
    public float CurrentLevel { get; private set; }

    //Maybe do this another way. They think of it as a happiness bonus, even though it's more of a decay rate multiplier. Talk about it.
    public float decayMultiplier = 1;

    private float decayPerSecond = 0.05f;
    private float maxLevel = 1;


    public void ReceiveLove(float loveAmount)
    {
        CurrentLevel = Mathf.Min(maxLevel, CurrentLevel + loveAmount);
    }

    public void SetStats(TowersonaStats stats)
    {
        decayPerSecond = stats.loveDecayPerSecond;
    }

    public void Reset()
    {
        CurrentLevel = maxLevel;
    }


    private void Update()
    {
        float decayThisStep = decayPerSecond * Time.deltaTime * decayMultiplier * NeedDecayRateManager.needDecayRateMultiplier;
        CurrentLevel = Mathf.Max(0, CurrentLevel - decayThisStep);
    }
}
