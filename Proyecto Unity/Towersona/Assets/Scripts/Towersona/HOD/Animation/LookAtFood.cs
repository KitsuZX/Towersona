using DG.Tweening;
using UnityEngine;

#pragma warning disable 649
public class LookAtFood : BaseAnimationPostProcesser
{
    [SerializeField] private Transform head;
    [SerializeField] private float weightTweenLength = 0.5f;

    private Sleeper sleeper;
    private Transform food;
    private float weight;
    private Tweener currentWeightTween;


    /// <summary>
    /// Must be set when creating the HOD.
    /// </summary>
    public Food Food
    {
        set
        {
            Draggable draggable = value.GetComponent<Draggable>();
            draggable.OnDragStart.AddListener(p => 
            {
                if (!sleeper.IsAsleep) TweenToWeight(1);
            });

            ReturnToPointAfterCountdown returnToPoint = value.GetComponent<ReturnToPointAfterCountdown>();
            returnToPoint.OnReturnedToPoint.AddListener(() =>
            {
                if (!sleeper.IsAsleep) TweenToWeight(0);
            });

            food = value.transform;
        }
    }

    public override void Execute()
    {
        if (!enabled) return;

        Quaternion targetRotation = Quaternion.LookRotation(food.position - head.position, head.up);
        head.rotation = Quaternion.Slerp(head.rotation, targetRotation, weight);
    }


    private void TweenToWeight(float targetWeight)
    {
        currentWeightTween.Kill();
        currentWeightTween = DOTween.To(() => weight, w => weight = w, targetWeight, weightTweenLength);
    }

    private void Awake()
    {
        sleeper = GetComponentInParent<Sleeper>();
    }

    private void Start()
    {
        //Subscribe to the necessary events
        Feedable[] feedables = GetComponentsInChildren<Feedable>();
        foreach (Feedable feedable in feedables)
        {
            feedable.OnFed += (food) => TweenToWeight(0);
        }

        sleeper.OnWentToSleep += () => TweenToWeight(0);
    }
}
