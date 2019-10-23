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


    private Vector3 TouchInWorldSpace
    {
        get
        {
            if (PointerInput.ReadPosition(out Vector2 touchPosition))
            {
                return camera.ScreenToWorldPoint(new Vector3(
                        touchPosition.x,
                        touchPosition.y,
                        transform.position.z - camera.GetComponent<Transform>().position.z));
            }
            else return Vector3.zero;
        }
    }

    private void OnMouseDown()
    {
        Vector3 touchInWorld = TouchInWorldSpace;
        touchOffset = transform.position - touchInWorld;

        isHeld = true;

        OnDragStart.Invoke();
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
                OnLetGo.Invoke();
            }
        }
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
}
