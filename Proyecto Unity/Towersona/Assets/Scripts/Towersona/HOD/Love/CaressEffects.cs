using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649
[RequireComponent(typeof(Caressable))]
public class CaressEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemPrefab;
    [SerializeField] private Vector3 particleSystemOffset;

    private new ParticleSystem particleSystem;

    private void StartPlaying()
    {
        particleSystem.Play();
    }

    private void StopPlaying()
    {
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }


    private void Start()
    {
        CreateParticleSystem();

        Caressable caressable = GetComponent<Caressable>();
        caressable.OnCaressStart += StartPlaying;
        caressable.OnCaressEnd += StopPlaying;
    }

    private void CreateParticleSystem()
    {
        particleSystem = Instantiate(particleSystemPrefab, transform, false);
        particleSystem.transform.localPosition = particleSystemOffset;

        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.playOnAwake = false;
        mainModule.loop = true;
    }
}
