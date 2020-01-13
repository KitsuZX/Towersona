using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649
[RequireComponent(typeof(Sleeper))]
public class ProceduralSleepAnimation : MonoBehaviour
{

    [SerializeField]
    Transform head;
    [SerializeField, Range(0, 90)]
    float headLowerAngleWhenAsleep;
    [Header("Go to sleep animation")]
    [SerializeField]
    AnimationCurve goToSleepCurve;
    [SerializeField]
    float goToSleepAnimationLength = 1f;
    [Header("Wake up animation")]
    [SerializeField]
    AnimationCurve wakeUpCurve;
    [SerializeField]
    float wakeUpAnimationLength = 0.5f;

    private SleepAnimationState currentState;
    private float timeAtLastStateChange;

    private void LateUpdate()
    {
        float t = Time.time - timeAtLastStateChange;
        switch (currentState)
        {
            case SleepAnimationState.GoingToSleep:
                if (t > goToSleepAnimationLength)
                {
                    StartState(SleepAnimationState.Sleeping);
                    goto case SleepAnimationState.Sleeping;     //Sí. He usado un goto. Fight me.
                }

                SetHeadRotation(goToSleepCurve.Evaluate(t / goToSleepAnimationLength) * headLowerAngleWhenAsleep);
                break;

            case SleepAnimationState.Sleeping:
                SetHeadRotation(headLowerAngleWhenAsleep);
                break;

            case SleepAnimationState.WakingUp:
                if (t > wakeUpAnimationLength)
                {
                    StartState(SleepAnimationState.Awake);
                    break;
                }

                SetHeadRotation(wakeUpCurve.Evaluate(t / wakeUpAnimationLength) * headLowerAngleWhenAsleep);
                break;

            default:
                break;
        }
    }

    private void SetHeadRotation(float rotation)
    {
        head.transform.localRotation = head.transform.localRotation * Quaternion.Euler(rotation, 0, 0);
    }

    private void StartState(SleepAnimationState newState)
    {
        currentState = newState;
        timeAtLastStateChange = Time.time;
    }

    private void Start()
    {
        Sleeper sleeper = GetComponent<Sleeper>();
        sleeper.OnWentToSleep += () => StartState(SleepAnimationState.GoingToSleep);
        sleeper.OnWokeUp += () => StartState(SleepAnimationState.WakingUp);
    }

    private enum SleepAnimationState
    {
        Awake,
        GoingToSleep,
        Sleeping,
        WakingUp
    }
}
