using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    public UnityEvent OnDragStart;
    public UnityEvent OnLetGo;

    [SerializeField]
    private float timeBeforeAutoBack = 1f;

    [HideInInspector]
    public Camera detailCamera;

    private new Transform transform;

    private Vector3 originalPosition;
    private Vector3 touchOffset;

    private bool isHeld = false;
    private bool wasDraggedLastFrame = false;
    private float timeWithoutDrag = 0;

    private Vector3 TouchInWorldSpace
    {
        get
        {
            if (Input.touchCount > 0)
            {
                return detailCamera.ScreenToWorldPoint(new Vector3(
                        Input.GetTouch(0).position.x,
                        Input.GetTouch(0).position.y,
                        transform.position.z - detailCamera.GetComponent<Transform>().position.z));
            }
            else return Vector3.zero;
        }
        
    }

    private void OnMouseDown()
    {
        if (Input.touchCount > 0)
        {
            originalPosition = transform.position;

            //This might fail when multiple cameras are present
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

        if (isHeld && Input.touchCount == 0)
        {
            OnLetGo.Invoke();
        }
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
        originalPosition = transform.position;
    }
}
