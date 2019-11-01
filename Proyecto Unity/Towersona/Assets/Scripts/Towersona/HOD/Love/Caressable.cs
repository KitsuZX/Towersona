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
    public event Action<float> OnCaressed;

    public bool IsBeingCaressed { get; private set; }

    private Vector2 pointerPosLastFrame;
    /// <summary>
    /// Mouse events are called before Update. We reset this flag each update. 
    /// If it's false at the start of Update, it means that there was no OnDrag call, or that it was not a caress.
    /// </summary>
    private bool wasCaressedThisFrame;


    public void OnBeginDrag(PointerEventData eventData)
    {
        StartCaress();

        pointerPosLastFrame = eventData.pressEventCamera.ScreenToViewportPoint(eventData.position);
        eventData.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Use the event no matter what
        eventData.Use();

        //Is the pointer is over the object?
        List<GameObject> hovered = eventData.hovered;
        bool pointerIsOverObject = hovered.Count > 0 && hovered[0] == gameObject;
        
        //If this is not a caress, return
        wasCaressedThisFrame = pointerIsOverObject;
        if (!wasCaressedThisFrame) return;

        //Figure out how much the pointer moved this frame.
        Camera raycastSourceCamera = eventData.pressEventCamera;
        Vector2 pointerPosThisFrame = raycastSourceCamera.ScreenToViewportPoint(eventData.position);
        float caressDistance = (pointerPosThisFrame - pointerPosLastFrame).magnitude;
        pointerPosLastFrame = pointerPosThisFrame;
        
        OnCaressed?.Invoke(caressDistance);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndCaress();
    }


    private void Update()
    {
        //Catch times where caressed state changes while the pointer is down.
        if (!wasCaressedThisFrame && IsBeingCaressed) EndCaress();
        else if (wasCaressedThisFrame && !IsBeingCaressed) StartCaress();

        wasCaressedThisFrame = false;
    }

    private void StartCaress()
    {
        IsBeingCaressed = true;
        OnCaressStart?.Invoke();
        print("Start");
    }

    private void EndCaress()
    {
        IsBeingCaressed = false;
        OnCaressEnd?.Invoke();
        print("End");
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
