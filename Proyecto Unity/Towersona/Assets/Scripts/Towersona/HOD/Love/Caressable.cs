using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Collider))]
public class Caressable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    public const string CARESSABLE_LAYER = "CaressableLayer";


    //Events
    public event Action OnCaressStart;
    public event Action OnCaressEnd;
    /// <summary>
    /// The seconds spent being caressed since last OnCaressed call is passed as an argument.
    /// </summary>
    public event Action<CaressEventData> OnCaressed;

    public bool IsBeingCaressed { get; private set; }


    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.Use();
        StartCaress();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Use the event no matter what
        eventData.Use();

        //Is the pointer is over the object?
        List<GameObject> hovered = eventData.hovered;
        bool pointerIsOverObject = hovered.Count > 0 && hovered[0] == gameObject;
        if (!pointerIsOverObject) return;

        StartCaress();

        CaressEventData caressEventData = new CaressEventData
        {
            caressTime = Time.deltaTime,
            caressPoint = eventData.pointerCurrentRaycast.worldPosition,
            caressable = this
        };
        OnCaressed?.Invoke(caressEventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        eventData.Use();
        EndCaress();
    }

    private void StartCaress()
    {
        if (!IsBeingCaressed)
        {
            IsBeingCaressed = true;
            OnCaressStart?.Invoke();
        }
    }

    private void EndCaress()
    {
        if (IsBeingCaressed)
        {
            IsBeingCaressed = false;
            OnCaressEnd?.Invoke();
        }
    }

    private void Awake()
    {
        int caressableLayer = LayerMask.NameToLayer(CARESSABLE_LAYER);
        if (gameObject.layer != caressableLayer)
        {
            Debug.LogWarning($"Caressable components must be in layer {CARESSABLE_LAYER}.", this);
            gameObject.layer = caressableLayer;
        }
    }


    public struct CaressEventData
    {
        public float caressTime;
        public Vector3 caressPoint;
        public Caressable caressable;
    }
}
