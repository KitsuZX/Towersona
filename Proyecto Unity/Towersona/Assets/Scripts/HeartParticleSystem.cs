using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartParticleSystem : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] particleSystems;

    bool isActive = false;

    public void Play()
    {
        if (!isActive)
        {
            isActive = true;

            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Play();
            }
        }
    }

    public void Stop()
    {
        if (isActive)
        {
            isActive = false;

            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}
