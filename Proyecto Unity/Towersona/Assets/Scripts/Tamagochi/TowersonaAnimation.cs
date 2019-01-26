using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaAnimation : MonoBehaviour
{
    public bool isFighting;
    public bool isCaressed;
    public bool hasEaten;
    public bool isLookingAtFood;

    public IdleState emotion;

    [Header("Animators")]
    [SerializeField]
    private Animator bodyAnimator;
    [SerializeField]
    private Animator headAnimator;
    [SerializeField]
    private Animator faceAnimator;


    #region Updating
    private void Update()
    {
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
    }

    public void TriggerEating()
    {
        hasEaten = true;
    }

    public void SetIsLookingAtFood(bool _isLookingAtFood)
    {
        isLookingAtFood = _isLookingAtFood;
    }
    #endregion


    public enum IdleState
    {
        Fine = 0, 
        Happy = 1,
        Hungry = 2,
        Missing = 3,
        Shit = 4
    }
}
