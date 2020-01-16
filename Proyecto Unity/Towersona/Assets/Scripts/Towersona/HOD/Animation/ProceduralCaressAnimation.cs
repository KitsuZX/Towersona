using UnityEngine;
using NaughtyAttributes;

#pragma warning disable 649
public class ProceduralCaressAnimation : BaseAnimationPostProcesser
{
    [SerializeField, Range(0, 1)] float slerpAmount = 0.5f;
    [SerializeField] float maxDegreesPerSecond = 30;
    [SerializeField, Required] Transform head;

    private Caressable.CaressEventData latestCaressEvent;
    private bool isPlaying = false;

    private Quaternion startRotation;
    private Quaternion lastTargetRotation;

    public override void Execute()
    {
        if (isPlaying && !latestCaressEvent.caressable.IsBeingCaressed) StopPlaying();
        else if (!isPlaying && (latestCaressEvent.caressable?.IsBeingCaressed ?? false)) StartPlaying();

        //Rotate the head
        if (weight > 0)
        {
            Vector3 lookDirection = latestCaressEvent.caressPoint - head.position;

            lookDirection = head.parent.InverseTransformDirection(lookDirection);
            lookDirection.x = -lookDirection.x;
            lookDirection.y = -lookDirection.y;

            Quaternion unlimitedTargetRotation = Quaternion.Slerp(startRotation, Quaternion.LookRotation(lookDirection, head.parent.up), slerpAmount);
            Quaternion limitedTargetRotation = Quaternion.RotateTowards(lastTargetRotation, unlimitedTargetRotation, maxDegreesPerSecond * Time.deltaTime);

            head.localRotation = Quaternion.Slerp(head.localRotation, limitedTargetRotation, weight);

            lastTargetRotation = limitedTargetRotation;
        }
    }

    
    private void StartPlaying()
    {
        isPlaying = true;
        TweenToWeight(1);
    }

    private void StopPlaying()
    {
        isPlaying = false;
        TweenToWeight(0);
    }
    

    private void Start()
    {
        Caressable[] caressables = GetComponentsInChildren<Caressable>();
        for (int i = 0; i < caressables.Length; i++)
        {
            caressables[i].OnCaressed += e => latestCaressEvent = e;
        }

        startRotation = head.localRotation;
    }
}
