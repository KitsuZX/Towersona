using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Feedable : MonoBehaviour
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

        GetComponent<Collider>().isTrigger = true;
    }

    private void Start()
    {
        Sleeper sleeper = GetComponentInParent<Sleeper>();
        sleeper.OnWentToSleep += () => enabled = false;
        sleeper.OnWokeUp += () => enabled = true;
    }
}
