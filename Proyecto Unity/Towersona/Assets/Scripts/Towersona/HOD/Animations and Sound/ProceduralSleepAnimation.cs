using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

#pragma warning disable 649
[RequireComponent(typeof(Sleeper))]
public class ProceduralSleepAnimation : MonoBehaviour
{
    private const float IDLE_SPEED_TWEEN_LENGTH = 1;

    [SerializeField] Transform head;
    [SerializeField] Animator bodyAnimator;
    [SerializeField, Range(0, 90)] float headLowerAngleWhenAsleep;
    [SerializeField, Range(0, 1)] float asleepIdleSpeed = 0.5f;

    [Header("Go to sleep animation")]
    [SerializeField] AnimationCurve goToSleepCurve;
    [SerializeField] float goToSleepAnimationLength = 1f;

    [Header("Wake up animation")]
    [SerializeField] AnimationCurve wakeUpCurve;
    [SerializeField] float wakeUpAnimationLength = 0.5f;

    private int IDLE_SPEED_HASH = Animator.StringToHash("IdleSpeed");

    private SleepAnimationState currentState;
    private float timeAtLastStateChange;
    private Tweener currentTween;


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

                float normalizedT = t / goToSleepAnimationLength;
                SetHeadRotation(goToSleepCurve.Evaluate(normalizedT) * headLowerAngleWhenAsleep);
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

        switch (newState)
        {
            case SleepAnimationState.Awake:
            case SleepAnimationState.WakingUp:
                currentTween?.Kill();
                currentTween = DOTween.To(() => bodyAnimator.GetFloat(IDLE_SPEED_HASH), v => bodyAnimator.SetFloat(IDLE_SPEED_HASH, v), 1, IDLE_SPEED_TWEEN_LENGTH);
                break;
            case SleepAnimationState.GoingToSleep:
            case SleepAnimationState.Sleeping:
                currentTween?.Kill();
                currentTween = DOTween.To(() => bodyAnimator.GetFloat(IDLE_SPEED_HASH), v => bodyAnimator.SetFloat(IDLE_SPEED_HASH, v), asleepIdleSpeed, IDLE_SPEED_TWEEN_LENGTH);
                break;
        }
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
