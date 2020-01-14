using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

#pragma warning disable 649
public class LookAtFood : BaseAnimationPostProcesser
{
    [SerializeField, Required] private Transform head;
    [SerializeField] private float weightTweenLength = 0.5f;

    public bool IsLookingAtFood { get; private set; }

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
                if (!sleeper.IsAsleep) StartLookingAtFood();
            });

            ReturnToPointAfterCountdown returnToPoint = value.GetComponent<ReturnToPointAfterCountdown>();
            returnToPoint.OnReturnedToPoint.AddListener(() =>
            {
                if (!sleeper.IsAsleep) StopLookingAtFood();
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


    private void StartLookingAtFood()
    {
        IsLookingAtFood = true;
        TweenToWeight(1);
    }

    private void StopLookingAtFood()
    {
        IsLookingAtFood = false;
        TweenToWeight(0);
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
            feedable.OnFed += (food) => StopLookingAtFood();
        }

        sleeper.OnWentToSleep += StopLookingAtFood;
    }
}
