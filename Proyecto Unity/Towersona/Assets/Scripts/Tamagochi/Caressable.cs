using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(TowersonaNeeds))]
public class Caressable : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject heartsEffect;

    [Header("Parameters")]

    [Header("Events")]
    public UnityEvent OnCaressStart;
    public UnityEvent OnCaressEnd;

    [SerializeField][Range(0.05f, 0.3f)]
    private float loveIncreasePerDeltaUnit = 0.1f;

    private TowersonaNeeds towersonaNeeds;
    private bool isBeingCaressed = false;

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
                Vector2 touchDelta = Input.mousePosition;

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

    private void OnMouseDrag()
    {
        if (!isBeingCaressed)
        {
            //Hearts particle effecct
            Vector3 pos = transform.position;
            pos.y += 1f;
            pos.z += 3f;
            GameObject effect = Instantiate(heartsEffect, pos, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(effect, 5f);

            isBeingCaressed = true;         
    
            OnCaressStart.Invoke();
        }

        float caressDistance = TouchDelta.magnitude;

        towersonaNeeds.ChangeNeedLevel(TowersonaNeeds.NeedType.Love, caressDistance * loveIncreasePerDeltaUnit);
    }

    private void OnMouseUp()
    {
        isBeingCaressed = false;
        OnCaressEnd.Invoke();
    }

    private void Awake()
    {
        towersonaNeeds = GetComponent<TowersonaNeeds>();
    }
}
