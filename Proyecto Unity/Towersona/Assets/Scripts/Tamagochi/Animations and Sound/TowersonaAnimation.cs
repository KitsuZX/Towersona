using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersonaAnimation : MonoBehaviour
{
    private bool isFighting;
    private bool isCaressed;
    private bool hasEaten;
    private bool isLookingAtFood;
    private IdleState emotion;

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

    [HideInInspector]
    public LookAwayFromTouch lookAway;

    private DetailedTowersonaSound sound;


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
        bodyAnimator.SetBool("isFighting", isFighting);
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
        sound.PlayEating();
    }

    public void SetIsLookingAtFood(bool _isLookingAtFood)
    {
        isLookingAtFood = _isLookingAtFood;
        lookAt.enabled = _isLookingAtFood;

        if (isLookingAtFood)
        {
            sound.PlayLookingAtFood();
        }
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
        sound = GetComponent<DetailedTowersonaSound>();
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
