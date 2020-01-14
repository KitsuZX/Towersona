using UnityEngine;
using DG.Tweening;

public abstract class BaseAnimationPostProcesser : MonoBehaviour
{
    [SerializeField] private float weightTweenLength = 0.5f;

    protected float weight;
    private Tweener currentWeightTween;

    public abstract void Execute();

    protected void TweenToWeight(float targetWeight)
    {
        currentWeightTween.Kill();
        currentWeightTween = DOTween.To(() => weight, w => weight = w, targetWeight, weightTweenLength);
    }
}
