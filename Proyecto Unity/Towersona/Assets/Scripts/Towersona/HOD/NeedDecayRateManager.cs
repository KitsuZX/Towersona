using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedDecayRateManager : MonoBehaviour
{
    public static NeedDecayRateManager Instance { get; private set; }


    [SerializeField]
    private NeedDecayRateLookupTable lookupTable;


    public float NeedDecayRateMultiplier { get; private set; }

    
    private void OnTowersonaCountChanged(int newAmount)
    {
        if (!lookupTable) return;
        NeedDecayRateMultiplier = lookupTable.GetDecayRateMultiplier(newAmount);
    }


    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        if (lookupTable) NeedDecayRateMultiplier = lookupTable.GetDecayRateMultiplier(GlobalTowersonaNeedProvider.GetAll().Count);
        GlobalTowersonaNeedProvider.OnNumberChanged += OnTowersonaCountChanged;
    }
}
