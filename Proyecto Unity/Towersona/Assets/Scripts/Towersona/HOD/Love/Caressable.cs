using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Collider))]
public class Caressable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    public const string CARESSABLE_LAYER = "CaressableLayer";

    [SerializeField]
    private float timeBeforeCaressEnds = 0.5f;

    //Events
    public event Action OnCaressStart;
    public event Action OnCaressEnd;
    /// <summary>
    /// The seconds spent being caressed since last OnCaressed call is passed as an argument.
    /// </summary>
    public event Action<float> OnCaressed;

    public bool IsBeingCaressed { get; private set; }

    private float timeWithoutCaress;


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
        OnCaressed?.Invoke(Time.deltaTime);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        eventData.Use();
        EndCaress();
    }


    private void Update()
    {
        if (IsBeingCaressed)
        {
            timeWithoutCaress += Time.deltaTime;
            if (timeWithoutCaress > timeBeforeCaressEnds)
            {
                EndCaress();
            }
        }
    }

    private void StartCaress()
    {
        if (!IsBeingCaressed)
        {
            IsBeingCaressed = true;
            OnCaressStart?.Invoke();
            timeWithoutCaress = 0;
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
}
