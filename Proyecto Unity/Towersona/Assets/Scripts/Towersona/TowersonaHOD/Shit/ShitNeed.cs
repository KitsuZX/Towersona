using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShitNeed : MonoBehaviour
{
    public UnityEvent OnTakenAShit;

	[Range(0, 5)]
	public float timeOffsetToTakeAShit = 1f;

    [Header("Shitting")] 
    private float shittingInterval = 20f;
    [SerializeField]
    private Shit shitPrefab = null;   

    [HideInInspector]
    public Transform[] shitSpawnPositions; 

    private int maxShitCount = 4;
    private float happinessImpactPerShit = 0.4f;
	private TowersonaHODAnimation towersonaAnimation;

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

		towersonaAnimation = GetComponent<TowersonaHODAnimation>();
    }

    private void AssignStats()
    {
        happinessImpactPerShit = stats.angerPerShit;
        shittingInterval = stats.timeBetweenShits;
        maxShitCount = stats.maxShits;
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
		if (timeSinceLastShit > shittingInterval)
		{
			TakeAShit();
		}
    }

    [Button]
    private void TakeAShit()
    {
		towersonaAnimation.TakeAShit();
		PurgeShitList();
        if (shits.Count >= maxShitCount) return;

		Invoke("Shit", timeOffsetToTakeAShit);

        timeSinceLastShit = 0;
    }

    private void PurgeShitList()
    {
        for (int i = shits.Count - 1; i >= 0; i--)
        {
            if (shits[i] == null) shits.RemoveAt(i);
        }
    }	
	
	private void Shit() {
		Vector3 position = shitSpawnPositions[shits.Count].position;

		Shit newShit = Instantiate(shitPrefab, position, Random.rotationUniform);
		newShit.origin = this;
		newShit.transform.SetParent(transform, true);
		shits.Add(newShit);

		OnTakenAShit.Invoke();
	}
}

