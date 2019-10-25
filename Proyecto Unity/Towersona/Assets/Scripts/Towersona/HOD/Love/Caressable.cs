using System;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Caressable : MonoBehaviour
{ 
    public const string CARESSABLE_LAYER = "CaressableLayer";

    //Events
    public event Action OnCaressStart;
    public event Action OnCaressEnd;

    public Camera RaycastCamera { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool IsBeingCaressed { get; private set; }


    private Vector2 TouchDelta
    {
        get
        {
            if (Input.touchCount > 0)
            {
                //Touch position in screen coordinates
                Vector2 touchDelta = Input.GetTouch(0).deltaPosition;

                //Convert to viewport coordinates
                touchDelta.x = touchDelta.x / Screen.width;
                touchDelta.y = touchDelta.y / Screen.height;

                return touchDelta;
            }

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                //Touch position in screen coordinates
                Vector2 touchDelta = (Vector2) Input.mousePosition - lastMousePosition;

                //Convert to viewport coordinates
                touchDelta.x = touchDelta.x / Screen.width;
                touchDelta.y = touchDelta.y / Screen.height;

                return touchDelta;
            }
#endif
            else
            {
                return Vector2.zero;
            }
        }
    }

    /*
    private void OnMouseDrag()
    {
        print("Drraaaag");

        if (!IsBeingCaressed)
        {
            OnCaressStart?.Invoke();
            IsBeingCaressed = true;
        }

        float caressDistance = TouchDelta.magnitude;
        //towersonaNeeds.LoveNeed.ReceiveLove(caressDistance * loveIncreasePerDeltaUnit);
    }
    */
    /*
    private void OnMouseUp()
    {
        if (IsBeingCaressed) OnCaressEnd.Invoke();
        IsBeingCaressed = false;
    }*/

#if UNITY_EDITOR
    //Save the lastMousePosition so that we can simulate delta touch with the mouse
    private Vector2 lastMousePosition;

    private void Update()
    {
        lastMousePosition = Input.mousePosition;
    }
#endif

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
