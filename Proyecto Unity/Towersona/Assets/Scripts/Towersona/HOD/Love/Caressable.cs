using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NaughtyAttributes;


[RequireComponent(typeof(Collider))]
public class Caressable : MonoBehaviour, IDragHandler
{ 
    public const string CARESSABLE_LAYER = "CaressableLayer";

    [Tooltip("Las caricias ocurren al mover el dedo. ¿Cuánto tiempo pasa desde que no mueves el dedo hasta que se considera que no estás acariciando?")]
    public float timeBeforeNoCaress = 0.5f;

    //Events
    /// <summary>
    /// The seconds spent being caressed since last OnCaressed call is passed as an argument.
    /// </summary>
    public event Action<CaressEventData> OnCaressed;

    [ShowNativeProperty]
    public bool IsBeingCaressed => latestCaressTime > Time.time - timeBeforeNoCaress; 

    private float latestCaressTime = float.NegativeInfinity;


    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.used) return;

        //Use the event no matter what
        eventData.Use();

        //Is the pointer is over the object?
        List<GameObject> hovered = eventData.hovered;
        bool pointerIsOverObject = false;
        for (int i = 0; i < hovered.Count && !pointerIsOverObject; i++)
        {
            pointerIsOverObject = hovered[i] == gameObject;
        }
        if (!pointerIsOverObject) return;
        

        latestCaressTime = Time.time;

        CaressEventData caressEventData = new CaressEventData
        {
            caressLength = Time.deltaTime,
            caressPoint = eventData.pointerCurrentRaycast.worldPosition,
            caressable = this
        };
        OnCaressed?.Invoke(caressEventData);
    }

    private void Awake()
    {
        int caressableLayer = LayerMask.NameToLayer(CARESSABLE_LAYER);
        if (gameObject.layer != caressableLayer)
        {
            Debug.LogWarning($"Caressable components must be in layer {CARESSABLE_LAYER}.", this);
            gameObject.layer = caressableLayer;
        }

        GetComponent<Collider>().isTrigger = true;
    }


    public struct CaressEventData
    {
        public float caressLength;
        public Vector3 caressPoint;
        public Caressable caressable;
    }
}
