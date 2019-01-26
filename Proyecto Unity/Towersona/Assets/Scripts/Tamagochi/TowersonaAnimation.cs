﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaAnimation : MonoBehaviour
{
    //TMP
    public bool TMP_isFighting;
    public bool TMP_isCaressed;
    public bool TMP_hasEaten;
    public bool TMP_isLookingAtFood;

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
    }

    private void UpdateBody()
    {
        bodyAnimator.SetBool("isFighting", TMP_isFighting);
    }

    private void UpdateHead()
    {
        if (TMP_hasEaten)
        {
            TMP_hasEaten = false;
            headAnimator.SetBool("hasEaten", true);
        }

        headAnimator.SetBool("isCaressed", TMP_isCaressed);
        headAnimator.SetBool("isLookingAtFood", TMP_isLookingAtFood);
        headAnimator.SetBool("isFighting", TMP_isFighting);
    }

    private void UpdateFace()
    {
        faceAnimator.SetInteger("idleEmotion", (int)emotion);
        faceAnimator.SetBool("isFighting", TMP_isFighting);
        faceAnimator.SetBool("hasEaten", TMP_hasEaten);
        faceAnimator.SetBool("isLookingAtFood", TMP_isLookingAtFood);
        faceAnimator.SetBool("isCaressed", TMP_isCaressed);
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
