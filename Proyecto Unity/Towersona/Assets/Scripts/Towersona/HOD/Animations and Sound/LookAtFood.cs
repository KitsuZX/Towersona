using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

#pragma warning disable 649
public class LookAtFood : MonoBehaviour, ISleepSusceptible
{
    [SerializeField] private Transform head;
    [SerializeField] private float weightTweenLength = 0.5f;

    /// <summary>
    /// Must be set when creating the HOD.
    /// </summary>
    public Food Food
    {
        set
        {
            ClearLookAtSources();
            ConstraintSource source = new ConstraintSource
            {
                sourceTransform = value.transform,
                weight = 1
            };
            lookAt.AddSource(source);

            Draggable draggable = value.GetComponent<Draggable>();
            draggable.OnDragStart.AddListener(p => SetLookAtActive(true));

            ReturnToPointAfterCountdown returnToPoint = value.GetComponent<ReturnToPointAfterCountdown>();
            returnToPoint.OnReturnedToPoint.AddListener(() => SetLookAtActive(false));
        }
    }

    private LookAtConstraint lookAt;

    
    private void ClearLookAtSources()
    {
        for (int i = 0; i < lookAt.sourceCount; i++)
        {
            lookAt.RemoveSource(i);
        }
    }

    private void SetLookAtActive(bool active)
    {
        if (active) DOTween.To(() => lookAt.weight, w => lookAt.weight = w, 1f, weightTweenLength);
        else DOTween.To(() => lookAt.weight, w => lookAt.weight = w, 0f, weightTweenLength);
    }


    private void Awake()
    {
        lookAt = head.gameObject.AddComponent<LookAtConstraint>();
        lookAt.constraintActive = true;
        lookAt.weight = 0;
        lookAt.useUpObject = false;
        lookAt.roll = 0;
    }

    private void Start()
    {
        Feedable[] feedables = GetComponentsInChildren<Feedable>();
        foreach (Feedable feedable in feedables)
        {
            feedable.OnFed += (food) => SetLookAtActive(false);
        }
        
    }

    private void OnEnable()
    {
        lookAt.enabled = true;
    }

    private void OnDisable()
    {
        lookAt.enabled = false;
    }
}
