using NaughtyAttributes;
using UnityEngine;

#pragma warning disable 649
[RequireComponent(typeof(Caressable))]
public class CaressEffects : MonoBehaviour
{
    [SerializeField, Required] private ParticleSystem particleSystemPrefab;
    [SerializeField] private Vector3 particleSystemOffset = new Vector3(0, 0, -1);

    private Caressable caressable;
    private ParticleSystem.EmissionModule particleEmission;


    private void Update()
    {
        particleEmission = GetComponentInChildren<ParticleSystem>().emission;
        particleEmission.enabled = caressable.IsBeingCaressed;
    }


    private void Start()
    {
        CreateParticleSystem();

        caressable = GetComponent<Caressable>();
    }

    private void CreateParticleSystem()
    {
        ParticleSystem particleSystem = Instantiate(particleSystemPrefab, transform, false);
        particleSystem.transform.localPosition = particleSystemOffset;

        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.playOnAwake = false;
        mainModule.loop = true;

        particleEmission = particleSystem.emission;
        particleEmission.enabled = false;

        particleSystem.Play();
    }
}
