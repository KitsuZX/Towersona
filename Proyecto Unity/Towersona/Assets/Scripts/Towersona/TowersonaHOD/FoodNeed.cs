using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNeed : ATowersonaNeed
{

    public override void SetStats(TowersonaStats stats)
    {
        maxLevel = stats.maxFood;
        decayPerSecond = stats.foodDecayPerSecond;
    }

    public override void Reset()
    {
        CurrentLevel = maxLevel;
    }

}
