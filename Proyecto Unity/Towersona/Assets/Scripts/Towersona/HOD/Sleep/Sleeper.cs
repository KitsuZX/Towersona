using System;
using UnityEngine;

/// <summary>
/// Finds all ISleepSusceptible components at Start. When going to sleep, it disables them. It enables them when waking up.
/// </summary>
public class Sleeper : MonoBehaviour
{
    public event Action OnWentToSleep;
    public event Action OnWokeUp;

    public bool IsAsleep { get; private set; }

    private ISleepSusceptible[] sleepSusceptibles;

    public void SetStats(TowersonaStats stats)
    {

    }

    public void GoToSleep()
    {
        if (IsAsleep) return;

        IsAsleep = true;
        OnWentToSleep?.Invoke();

        for (int i = 0; i < sleepSusceptibles.Length; i++)
        {
            if (sleepSusceptibles[i] != null) sleepSusceptibles[i].enabled = false;
        }
    }

    public void WakeUp()
    {
        if (!IsAsleep) return;

        IsAsleep = false;
        OnWokeUp?.Invoke();

        for (int i = 0; i < sleepSusceptibles.Length; i++)
        {
            if (sleepSusceptibles[i] != null) sleepSusceptibles[i].enabled = true;
        }
    }


    private void Start()
    {
        if (SleepDirector.Instance) SleepDirector.Instance.Register(this);

        sleepSusceptibles = GetComponentsInChildren<ISleepSusceptible>();
    }

    private void OnDestroy()
    {
        if (SleepDirector.Instance) SleepDirector.Instance.Unregister(this);
    }
}
