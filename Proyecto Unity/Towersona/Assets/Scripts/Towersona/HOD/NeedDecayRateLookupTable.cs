using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649
[CreateAssetMenu(menuName ="Need Decay Rate Lookup Table", fileName ="New NeedDecayRateLookupTable")]
public class NeedDecayRateLookupTable : ScriptableObject
{
    [SerializeField, Tooltip("Los valores por los que se multiplica cuánto bajan las necesidades. " +
        "Cada valor se corresponde con el índice + 1. Es decir, el elemento 0 (el primero) es el multiplicador para 1 towersona.")]
    private float[] multiplierPerTowersonaAmount;

    public float GetDecayRateMultiplier(int towersonaAmount)
    {
        if (towersonaAmount > multiplierPerTowersonaAmount.Length)
        {
            return multiplierPerTowersonaAmount[multiplierPerTowersonaAmount.Length - 1];
        }
        else if (towersonaAmount <= 0)
        {
            if (towersonaAmount < 0) Debug.LogWarning($"Requested a need decay rate multiplier for {towersonaAmount} towersonas. That's nonsense.");
            return 1;
        }
        else
        {
            return multiplierPerTowersonaAmount[towersonaAmount - 1];
        }
    }
}