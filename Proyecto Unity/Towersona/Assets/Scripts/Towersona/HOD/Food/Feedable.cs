using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class Feedable : MonoBehaviour, ISleepSusceptible
{
    public const string FEEDABLE_LAYER_NAME = "FeedableLayer";

    public event Action<Food> OnFed;

    public void Feed(Food food)
    {
        OnFed?.Invoke(food);
    }


    private void Awake()
    {
        int feedableLayer = LayerMask.NameToLayer(FEEDABLE_LAYER_NAME);
        if (gameObject.layer != feedableLayer)
        {
            Debug.LogWarning($"Feedable components must be in layer {FEEDABLE_LAYER_NAME}.", this);
            gameObject.layer = feedableLayer;
        }
    }
}
