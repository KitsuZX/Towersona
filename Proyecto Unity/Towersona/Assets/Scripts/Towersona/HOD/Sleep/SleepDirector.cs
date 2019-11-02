using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SleepDirector : MonoBehaviour
{
    public static SleepDirector Instance { get; private set; }

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
    public void SleepOneAtRandom()
    {
        if (sleepers.Count == 0) return;

        int index = Random.Range(0, sleepers.Count);
        sleepers[index].GoToSleep();
    }


    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }
}
