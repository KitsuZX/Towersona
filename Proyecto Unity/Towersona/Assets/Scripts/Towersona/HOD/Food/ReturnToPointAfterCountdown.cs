using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class ReturnToPointAfterCountdown : MonoBehaviour
{
    #region Inspector
    public float countdownLength = 1;
    public float returnTweenLength = 0.5f;
    public bool inWorldSpace;
    [HideIf("autoSetStartingPosition")] public Vector3 returnPoint;

    [SerializeField] private bool autoSetStartingPosition = true;

    public UnityEvent OnReturnedToPoint;
    #endregion

    private new Transform transform;
    private WaitForSeconds wait;

    #region Countdown Activation
    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    public void CancelCountdown()
    {
        StopAllCoroutines();
    }
    #endregion

    private IEnumerator CountdownCoroutine()
    {
        yield return wait;

        ReturnToPoint();
    }

    private void ReturnToPoint()
    {
        if (returnTweenLength > 0)
        {
            DOTween.Kill(transform);
            if (inWorldSpace) transform.DOMove(returnPoint, returnTweenLength);
            else transform.DOLocalMove(returnPoint, returnTweenLength);
        }
        else
        {
            if (inWorldSpace) transform.position = returnPoint;
            else transform.localPosition = returnPoint;
        }

        OnReturnedToPoint.Invoke();
    }


    private void Awake()
    {
        transform = GetComponent<Transform>();
        wait = new WaitForSeconds(countdownLength);
    }

    private void Start()
    {
        if (autoSetStartingPosition) returnPoint = (inWorldSpace) ? transform.position : transform.localPosition;
    }
}
