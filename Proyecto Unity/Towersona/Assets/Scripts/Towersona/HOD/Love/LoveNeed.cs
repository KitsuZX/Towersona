using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveNeed : MonoBehaviour
{
    public const float MAX_LEVEL = 1;

    public float CurrentLevel { get; private set; }

    //Maybe do this another way. They think of it as a happiness bonus, even though it's more of a decay rate multiplier. Talk about it.
    //Where should the buff be applied from? I don't know the callstack here.
    public float decayMultiplier = 1;

    private float decayPerSecond = 0.05f;


    public void ReceiveLove(float loveAmount)
    {
        CurrentLevel = Mathf.Min(MAX_LEVEL, CurrentLevel + loveAmount);
    }


    public void SetStats(TowersonaStats stats)
    {
        decayPerSecond = stats.loveDecayPerSecond;
    }

    public void ResetNeed()
    {
        CurrentLevel = MAX_LEVEL;
    }


    private void Update()
    {
        float decayThisStep = decayPerSecond * Time.deltaTime * decayMultiplier * NeedDecayRateManager.needDecayRateMultiplier;
        CurrentLevel = Mathf.Max(0, CurrentLevel - decayThisStep);
    }

}
