using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNeed : MonoBehaviour
{
    public float CurrentLevel { get; protected set; }

    private float decayPerSecond = 0.05f;
    public float MaxLevel { get; private set; }


    public void SetStats(TowersonaStats stats)
    {
        MaxLevel = stats.maxFood;
        decayPerSecond = stats.foodDecayPerSecond;
    }

    public void ResetNeed()
    {
        CurrentLevel = MaxLevel;
    }


    /// <summary>
    /// Does the need decay. Doesn't take towersona count into account.
    /// </summary>
    private void Update()
    {
        float decayThisStep = decayPerSecond * Time.deltaTime;
        CurrentLevel = Mathf.Max(0, CurrentLevel - decayThisStep);
    }

    private void Eat(Food food)
    {
        CurrentLevel = Mathf.Min(MaxLevel, CurrentLevel + food.HungerFulmilmentPerRation);
    }

    /// <summary>
    /// Subscribe to Feedable's OnFed event.
    /// </summary>
    private void Start()
    {
        Feedable[] feedables = GetComponentsInChildren<Feedable>();
        for (int i = 0; i < feedables.Length; i++)
        {
            feedables[i].OnFed += Eat;
        }

        if (feedables.Length == 0)
        {
            Debug.LogWarning("No se ha encontrado ningún componente Feedable en el HOD. " +
                "A no ser que se haya diseñado una Towersona que tenga hambre pero no coma, esto es un error.", this);
        }
    }
}
