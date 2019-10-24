using System;
using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    public DraggableEvent OnDragStart;
    public DraggableEvent OnLetGo;


    /// <summary>
    /// The Camera from which the pointer's world position should be calculated.
    /// </summary>
    public Camera CasterCamera {
        get => _casterCamera;
        set
        {
            _casterCamera = value;
            cameraTransform = _casterCamera.GetComponent<Transform>();
        }
    }
    private Camera _casterCamera;


    private Transform cameraTransform;
    private new Transform transform;


    private Vector3 touchOffset;
    private bool isHeld = false;


    private Vector3 TouchInWorldSpace
    {
        get
        {
            if (PointerInput.ReadPosition(out Vector2 touchPosition))
            {
                return CasterCamera.ScreenToWorldPoint(new Vector3(
                        touchPosition.x,
                        touchPosition.y,
                        transform.position.z - cameraTransform.position.z));
            }
            else return Vector3.zero;
        }
    }

    private void OnMouseDown()
    {
        Vector3 touchInWorld = TouchInWorldSpace;
        touchOffset = transform.position - touchInWorld;

        isHeld = true;

        OnDragStart.Invoke(new DraggableEventArgs { cameraPosition = cameraTransform.position });
    }

    private void Update()
    {
        if (isHeld)
        {
            if (PointerInput.IsTouching)
            {
                Vector3 tmp = TouchInWorldSpace + touchOffset;
                transform.position = tmp;
            }
            else
            {
                isHeld = false;
                OnLetGo.Invoke(new DraggableEventArgs { cameraPosition = cameraTransform.position });
            }
        }
    }


    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    [Serializable]
    public class DraggableEvent : UnityEvent<DraggableEventArgs>
    {

    }

    public struct DraggableEventArgs
    {
        public Vector3 cameraPosition;
    }
}
