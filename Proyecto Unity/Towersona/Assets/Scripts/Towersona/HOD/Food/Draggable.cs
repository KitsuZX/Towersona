using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //Inspector
    public DraggableEvent OnDragStart;
    public DraggableEvent OnLetGo;

    public bool IsBeingDragged { get; private set; }

    //Drag data
    private Camera raycastCamera;
    private Vector3 touchOffset;
    private float raycastDepth;
    
    private new Transform transform;    

    #region Pointer Events
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 currentPosition = transform.position;
        Vector3 touchInWorld = eventData.pointerCurrentRaycast.worldPosition;

        touchOffset = currentPosition - touchInWorld;

        IsBeingDragged = true;
        raycastCamera = eventData.pressEventCamera;
        raycastDepth = transform.position.z - raycastCamera.transform.position.z;

        OnDragStart.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = GetCameraAlignedTouchPosition(eventData.position);
        transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsBeingDragged = false;
        OnLetGo.Invoke(eventData);
    }
    #endregion

    private Vector3 GetCameraAlignedTouchPosition(Vector2 screenPosition)
    {
        return raycastCamera.ScreenToWorldPoint(new Vector3(
                screenPosition.x,
                screenPosition.y,
                raycastDepth));
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    [Serializable]
    public class DraggableEvent : UnityEvent<PointerEventData> { }
}
