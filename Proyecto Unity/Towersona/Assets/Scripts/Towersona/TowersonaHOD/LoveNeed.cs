using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveNeed : ATowersonaNeed
{
    public override void SetStats(TowersonaStats stats)
    {
        decayPerSecond = stats.loveDecayPerSecond;
    }

    public override void Reset()
    {
        CurrentLevel = maxLevel;
    }

    //TODO: Implement some way to do love bonus?

    private void Awake()
    {
        maxLevel = 1;
    }
}
