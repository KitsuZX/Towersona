using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Inspector
    public DraggableEvent OnDragStart;
    public DraggableEvent OnLetGo;


    public Camera RaycastCamera {
        get => _raycastCamera;
        set
        {
            _raycastCamera = value;
            cameraTransform = _raycastCamera.GetComponent<Transform>();
        }
    }
    private Camera _raycastCamera;


    private Transform cameraTransform;
    private new Transform transform;

    private Vector3 touchOffset;
    private bool isHeld = false;

    #region Pointer Events
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 touchInWorld = eventData.pointerPressRaycast.worldPosition;
        //Vector3 touchInWorld = TouchInWorldSpace;
        touchOffset = transform.position - touchInWorld;

        isHeld = true;

        OnDragStart.Invoke(new DraggableEventArgs { cameraPosition = cameraTransform.position });
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        
        OnLetGo.Invoke(new DraggableEventArgs { cameraPosition = cameraTransform.position });
    }
    #endregion





    private Vector3 TouchInWorldSpace
    {
        get
        {
            if (PointerInput.ReadPosition(out Vector2 touchPosition))
            {
                return RaycastCamera.ScreenToWorldPoint(new Vector3(
                        touchPosition.x,
                        touchPosition.y,
                        transform.position.z - cameraTransform.position.z));
            }
            else return Vector3.zero;
        }
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
