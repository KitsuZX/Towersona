using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersonaAnimation : MonoBehaviour
{
    public bool isFighting;
    public bool isCaressed;
    public bool hasEaten;
    public bool isLookingAtFood;

    public IdleState emotion;

    [SerializeField]
    private Slider happinessSlider;


    [Header("Animators")]
    [SerializeField]
    private Animator bodyAnimator;
    [SerializeField]
    private Animator headAnimator;
    [SerializeField]
    private Animator faceAnimator;
    
    private TowersonaNeeds needs;

    [SerializeField]
    private LookAt lookAt;
    [SerializeField]
    private LookAwayFromTouch lookAway;


    #region Updating
    private void Update()
    {
        ChooseEmotion();

        UpdateBody();
        UpdateHead();
        UpdateFace();


        if (hasEaten) hasEaten = false;
    }

    private void UpdateBody()
    {
        if (hasEaten)
        {
            headAnimator.SetBool("hasEaten", true);
        }

        headAnimator.SetBool("isCaressed", isCaressed);
        headAnimator.SetBool("isLookingAtFood", isLookingAtFood);
        headAnimator.SetBool("isFighting", isFighting);

    }

    private void UpdateHead()
    {
        if (hasEaten)
        {
            headAnimator.SetBool("hasEaten", true);
        }

        headAnimator.SetBool("isCaressed", isCaressed);
        headAnimator.SetBool("isLookingAtFood", isLookingAtFood);
        headAnimator.SetBool("isFighting", isFighting);
    }

    private void UpdateFace()
    {
        

        faceAnimator.SetInteger("idleEmotion", (int)emotion);
        if (hasEaten) faceAnimator.SetBool("hasEaten", true);
        faceAnimator.SetBool("isLookingAtFood", isLookingAtFood);
        faceAnimator.SetBool("isCaressed", isCaressed);
    }
    #endregion

    #region Info Gathering
    public void SetIsCaressed(bool isBeingCaressed)
    {
        isCaressed = isBeingCaressed;
        lookAway.enabled = isBeingCaressed;
    }

    public void TriggerEating()
    {
        hasEaten = true;
    }

    public void SetIsLookingAtFood(bool _isLookingAtFood)
    {
        isLookingAtFood = _isLookingAtFood;
        lookAt.enabled = _isLookingAtFood;
    }

    private void ChooseEmotion()
    {
        TowersonaNeeds.NeedType notifiedNeed = needs.CheckIfShouldNotifyNeed();

        if (notifiedNeed == TowersonaNeeds.NeedType.None)
        {
            if (needs.HappinessLevel > 1) emotion = IdleState.Happy;
            else emotion = IdleState.Fine;
        }
        else
        {
            if (notifiedNeed == TowersonaNeeds.NeedType.Hunger) emotion = IdleState.Hungry;
            else if (notifiedNeed == TowersonaNeeds.NeedType.Love) emotion = IdleState.Missing;
            else if (notifiedNeed == TowersonaNeeds.NeedType.Shit) emotion = IdleState.Shit;
        }
    }
    #endregion

    public void SetLookAtTransform(Transform tr)
    {
        lookAt.food = tr;
    }

    private void Awake()
    {
        needs = GetComponentInParent<TowersonaNeeds>();
    }

    public enum IdleState
    {
        Fine = 0, 
        Happy = 1,
        Hungry = 2,
        Missing = 3,
        Shit = 4
    }
}
