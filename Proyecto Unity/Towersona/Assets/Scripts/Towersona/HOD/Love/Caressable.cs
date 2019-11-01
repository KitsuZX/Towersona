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
    public event Action<float> OnCaressed;

    public bool IsBeingCaressed { get; private set; }

    private Vector2 pointerPosLastFrame;
    private float timeWithoutCaress;


    public void OnBeginDrag(PointerEventData eventData)
    {
        StartCaress();

        pointerPosLastFrame = eventData.pressEventCamera.ScreenToViewportPoint(eventData.position);
        eventData.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("OnDrag");
        //Use the event no matter what
        eventData.Use();

        //Is the pointer is over the object?
        List<GameObject> hovered = eventData.hovered;
        bool pointerIsOverObject = hovered.Count > 0 && hovered[0] == gameObject;
        if (!pointerIsOverObject) return;

        //Figure out how much the pointer moved this frame.
        Camera raycastSourceCamera = eventData.pressEventCamera;
        Vector2 pointerPosThisFrame = raycastSourceCamera.ScreenToViewportPoint(eventData.position);
        float caressDistance = (pointerPosThisFrame - pointerPosLastFrame).magnitude;
        pointerPosLastFrame = pointerPosThisFrame;

        StartCaress();
        OnCaressed?.Invoke(caressDistance);
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
