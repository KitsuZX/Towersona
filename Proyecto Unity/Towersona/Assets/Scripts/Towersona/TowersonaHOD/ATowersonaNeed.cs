using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance might be overkill here. We'll see.
//Yep, overkill. Kill it with fire.
public abstract class ATowersonaNeed : MonoBehaviour
{
    public float CurrentLevel { get; protected set; }

    protected float decayPerSecond = 0.05f;
    protected float maxLevel = 1;

    public abstract void SetStats(TowersonaStats stats);

    public abstract void Reset();

    private void Update()
    {
        float decay = decayPerSecond * (1 - pattern.HappinessBonus);

        loveDecay = Mathf.Max(0, loveDecay);

        hungerLevel -= hungerDecayPerSecond * Time.deltaTime;
        loveLevel -= loveDecay * Time.deltaTime;

        hungerLevel = Mathf.Max(0, hungerLevel);
        loveLevel = Mathf.Max(0, loveLevel);
    }
}
