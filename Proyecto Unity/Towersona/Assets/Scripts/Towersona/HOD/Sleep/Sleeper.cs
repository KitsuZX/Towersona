using System;
using UnityEngine;

/// <summary>
/// When going to sleep, it disables them. It enables them when waking up.
/// </summary>
public class Sleeper : MonoBehaviour
{
    public event Action OnWentToSleep;
    public event Action OnWokeUp;

    public bool IsAsleep { get; private set; }
    public float SleepChance { get; private set; }


    public void GoToSleep()
    {
        if (IsAsleep) return;

        IsAsleep = true;
        OnWentToSleep?.Invoke();
    }

    public void WakeUp()
    {
        if (!IsAsleep) return;

        IsAsleep = false;
        OnWokeUp?.Invoke();
    }

    public void SetStats(TowersonaStats stats)
    {
        SleepChance = stats.sleepChance;
    }


    private void Start()
    {
        if (SleepDirector.Instance) SleepDirector.Instance.Register(this);
    }

    private void OnDestroy()
    {
        if (SleepDirector.Instance) SleepDirector.Instance.Unregister(this);
    }
}