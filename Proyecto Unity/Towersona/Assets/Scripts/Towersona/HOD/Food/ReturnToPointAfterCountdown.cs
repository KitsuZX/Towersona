using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class ReturnToPointAfterCountdown : MonoBehaviour
{
    #region Inspector
    public float countdownLength = 1;
    public bool inWorldSpace;
    [HideIf("autoSetStartingPosition")] public Vector3 returnPoint;

    [SerializeField] private bool autoSetStartingPosition;

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
        if (inWorldSpace) transform.position = returnPoint;
        else transform.localPosition = returnPoint;

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
