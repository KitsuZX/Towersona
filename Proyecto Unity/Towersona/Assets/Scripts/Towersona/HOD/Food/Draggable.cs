using System;
using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    public UnityEvent OnDragStart;
    public UnityEvent OnLetGo;

    [NonSerialized]
    public new Camera camera;

    private new Transform transform;

    private Vector3 touchOffset;

    private bool isHeld = false;
    private bool wasDraggedLastFrame = false;

    private Vector3 TouchInWorldSpace
    {
        get
        {
            if (Input.touchCount > 0)
            {
                return camera.ScreenToWorldPoint(new Vector3(
                        Input.GetTouch(0).position.x,
                        Input.GetTouch(0).position.y,
                        transform.position.z - camera.GetComponent<Transform>().position.z));
            }

            //Mouse Controls
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                return camera.ScreenToWorldPoint(new Vector3(
                        Input.mousePosition.x,
                        Input.mousePosition.y,
                        transform.position.z - camera.GetComponent<Transform>().position.z));
            }

#endif
            else return Vector3.zero;
        }
        
    }

    private void OnMouseDown()
    {

#if UNITY_EDITOR
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
#else
        if (Input.touchCount > 0)
#endif
        {
            Vector3 touchInWorld = TouchInWorldSpace;

            touchOffset = touchInWorld - transform.position;//transform.position - touchInWorld;

            OnDragStart.Invoke();
            wasDraggedLastFrame = true;
            isHeld = true;
        }
    }

    private void OnMouseDrag()
    {
        wasDraggedLastFrame = true;
        Vector3 tmp = TouchInWorldSpace + touchOffset;
        transform.position = tmp;
    }

    private void OnMouseUpAsButton()
    {
        if (!isHeld) return;
        OnLetGo.Invoke();
        wasDraggedLastFrame = false;

        if (gameObject != null && originalPosition != Vector3.zero)
        {
            GoBack();
        }
        isHeld = false;
    }

    private void GoBack()
    {
        transform.position = originalPosition;
        wasDraggedLastFrame = false;
    }

    private void Update()
    {
        if (!wasDraggedLastFrame)
        {
            timeWithoutDrag += Time.deltaTime;
            if (timeWithoutDrag > timeBeforeAutoBack) GoBack();
        }
        else
        {
            timeWithoutDrag = 0;
        }
        wasDraggedLastFrame = false;

#if UNITY_EDITOR
        if (isHeld && (Input.touchCount == 0 && !Input.GetMouseButton(0)))
        {
            OnLetGo.Invoke();
        }
#else
        if (isHeld && Input.touchCount == 0)
        {
            OnLetGo.Invoke();
        }
#endif


    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
        originalPosition = transform.position;
    }
}
