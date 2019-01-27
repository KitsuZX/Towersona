using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartParticles : MonoBehaviour
{
    public ParticleSystem[] systems;

    private bool isActive;

    public void Play()
    {
        if (isActive) return;

        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].Play();
        }

        isActive = true;
    }

    public void Stop()
    {
        if (!isActive) return;

        for (int i = 0; i < systems.Length; i++)
        {
            systems[i].Stop();
        }

        isActive = false;
    }
}
