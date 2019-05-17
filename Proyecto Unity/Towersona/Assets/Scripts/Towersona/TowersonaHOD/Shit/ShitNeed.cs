using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShitNeed : MonoBehaviour
{
    public UnityEvent OnTakenAShit;

    [Header("Shitting")] 
    private float shittingInterval = 20f;
    [SerializeField]
    private Shit shitPrefab = null;   

    [HideInInspector]
    public Transform[] shitSpawnPositions; 

    private int maxShitCount = 4;
    private float happinessImpactPerShit = 0.4f;

    public float Level
    {
        get
        {
            return Mathf.Max(0, 1 - happinessImpactPerShit * shits.Count);
        }
    }
    

    private List<Shit> shits;
    private float timeSinceLastShit;
    private TowersonaStats stats;

    private void Awake()
    {
        shits = new List<Shit>();
    }

    private void Start()
    {     
        stats = GetComponentInParent<TowersonaHOD>().towersona.stats;
        AssignStats();
    }

    private void AssignStats()
    {
        happinessImpactPerShit = stats.enfadoPorMierda;
        shittingInterval = stats.tiempoEntreMierdas;
        maxShitCount = stats.maxMierdas;
    }

    public void CleanAllShit()
    {
        if (shits == null) return;

        for (int i = 0; i < shits.Count; i++)
        {
            shits[i].Clean();
        }
    }

    /// <summary>
    /// Should only be called by the shits themselves
    /// </summary>
    public void Remove(Shit shit)
    {
        shits.Remove(shit);
    }

    private void Update()
    {
        timeSinceLastShit += Time.deltaTime;
        if (timeSinceLastShit > shittingInterval) TakeAShit();
    }

    [Button]
    private void TakeAShit()
    {
        PurgeShitList();
        if (shits.Count >= maxShitCount) return;

        Vector3 position = shitSpawnPositions[shits.Count].position;

        Shit newShit = Instantiate(shitPrefab, position, Random.rotationUniform);
        newShit.origin = this;
        newShit.transform.SetParent(transform, true);
        shits.Add(newShit);
        timeSinceLastShit = 0;

        OnTakenAShit.Invoke();
    }

    private void PurgeShitList()
    {
        for (int i = shits.Count - 1; i >= 0; i--)
        {
            if (shits[i] == null) shits.RemoveAt(i);
        }
    }
}

