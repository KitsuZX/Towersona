using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    public UnityEvent OnLetGo;

    [HideInInspector]
    public Camera detailCamera;

    private new Transform transform;

    private Vector3 originalPosition;
    private Vector3 touchOffset;

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
        }
    }

    private void OnMouseDrag()
    {
        Vector3 tmp = TouchInWorldSpace + touchOffset;
        transform.position = tmp;
    }

    private void OnMouseUpAsButton()
    {
        OnLetGo.Invoke();

        if (gameObject != null && originalPosition != Vector3.zero)
        {
            GoBack();
        }
    }

    private void GoBack()
    {
        transform.position = originalPosition;
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
}
