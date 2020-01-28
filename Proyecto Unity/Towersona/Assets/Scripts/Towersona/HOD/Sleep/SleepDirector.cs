using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SleepDirector : MonoBehaviour
{
    public static SleepDirector Instance { get; private set; }
    [SerializeField, Required] SleepDirectorSettings settings;


    private List<Sleeper> sleepers = new List<Sleeper>();


    public void Register(Sleeper sleeper)
    {
        sleepers.Add(sleeper);
    }

    public void Unregister(Sleeper sleeper)
    {
        sleepers.Remove(sleeper);
    }


    [Button]
    private void SleepOneAtRandom()
    {
        float totalChance = 0;

        foreach (Sleeper sleeper in sleepers)
        {
            if (!sleeper.IsAsleep) totalChance += sleeper.SleepChance;
        }
        if (totalChance == 0) return;

        float result = Random.Range(0, totalChance);

        float counter = 0;
        foreach (Sleeper sleeper in sleepers)
        {
            if (sleeper.IsAsleep) continue;

            counter += sleeper.SleepChance;
            if (counter >= result)
            {
                Sleep(sleeper);
                return;
            }
        }
    }

    private void Sleep(Sleeper sleeper)
    {
        sleeper.GoToSleep();
    }

    private void ScheduleNextSleep()
    {
        float interval = settings.Interval;
        Invoke("SleepOneAtRandom", interval);
        Invoke("ScheduleNextSleep", interval);
    }


    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        ScheduleNextSleep();
    }
}
