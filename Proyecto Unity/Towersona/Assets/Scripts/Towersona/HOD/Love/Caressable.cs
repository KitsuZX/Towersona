using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(TowersonaNeeds))]
public class Caressable : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private GameObject heartsEffect = null;

	[Header("Parameters")]

    [Header("Events")]
    public UnityEvent OnCaressStart;
    public UnityEvent OnCaressEnd;

	[HideInInspector] public bool isBeingCaressed = false;

	private float loveIncreasePerDeltaUnit = 0.1f;

    private TowersonaNeeds towersonaNeeds;
    private TowersonaStats stats;

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

    private void Start()
    {
        //stats = GetComponentInParent<TowersonaHODSetup>().towersona.stats;
        AssignStats();
        towersonaNeeds = GetComponent<TowersonaNeeds>();
    }

    private void AssignStats()
    {
        loveIncreasePerDeltaUnit = stats.happinessPerPet;
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
            Destroy(effect, 5f);    //We could just sto the effect instead of destroying it, couldn't we? Garbage and all that.

            isBeingCaressed = true;         
    
            OnCaressStart.Invoke();
        }

        float caressDistance = TouchDelta.magnitude;
        towersonaNeeds.LoveNeed.ReceiveLove(caressDistance * loveIncreasePerDeltaUnit);
    }

    private void OnMouseUp()
    {
        isBeingCaressed = false;
        OnCaressEnd.Invoke();
    }
}
