using UnityEngine;
using NaughtyAttributes;

public class AnimationPostProcessController : MonoBehaviour
{
    [SerializeField, ReorderableList]
    public BaseAnimationPostProcesser[] postProcessers;


    private void LateUpdate()
    {
        for (int i = 0; i < postProcessers.Length; i++)
        {
            postProcessers?[i].Execute();
        }
    }
}
