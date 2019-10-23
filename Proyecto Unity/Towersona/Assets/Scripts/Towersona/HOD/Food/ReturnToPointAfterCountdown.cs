using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToPointAfterCountdown : MonoBehaviour
{
    public float countdownLength = 1;
    public Vector3 returnPoint;
    public bool inWorldSpace;

    public UnityEvent OnReturnedToPoint;

    private new Transform transform;
    private WaitForSeconds wait;


    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    public void CancelCountdown()
    {
        StopAllCoroutines();
    }


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
}
