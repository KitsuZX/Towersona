using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(TowersonaNeeds))]
public class Caressable : MonoBehaviour
{

    [SerializeField][Range(0.05f, 0.3f)]
    private float loveIncreasePerDeltaUnit = 0.1f;

    private TowersonaNeeds towersonaNeeds;




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
            else
            {
                return Vector2.zero;
            }
        }
    }

    private void OnMouseDrag()
    {
        float caressDistance = TouchDelta.magnitude;

        towersonaNeeds.ChangeNeedLevel(TowersonaNeeds.NeedType.Love, caressDistance * loveIncreasePerDeltaUnit);
    }

    private void Awake()
    {
        towersonaNeeds = GetComponent<TowersonaNeeds>();
    }
}
