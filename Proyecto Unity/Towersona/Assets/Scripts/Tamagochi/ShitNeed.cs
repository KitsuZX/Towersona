using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShitNeed : MonoBehaviour
{
    #region Inspector
    [Header("Happiness impact")]
    [SerializeField][Range(0, 1)]
    private float happinessImpactPerShit = 0.4f;

    [Header("Shitting")]
    [SerializeField][Tooltip("In seconds")]
    private float shittingInterval = 20f;
    [SerializeField]
    private Shit shitPrefab = null;

    public UnityEvent OnTakenAShit;
    public UnityEvent OnOneShitCleaned;
    public UnityEvent OnAllShitCleaned;
    #endregion

    public float Level
    {
        get
        {
            return Mathf.Max(0, 1 - happinessImpactPerShit * shits.Count);
        }
    }


    private List<Shit> shits;
    private float timeSinceLastShit;

    public void CleanAllShit()
    {
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
        Shit newShit = Instantiate(shitPrefab);
        newShit.origin = this;
        shits.Add(newShit);
        timeSinceLastShit = 0;
    }

    #region Initialization
    private void Awake()
    {
        shits = new List<Shit>();
    }
    #endregion
}

