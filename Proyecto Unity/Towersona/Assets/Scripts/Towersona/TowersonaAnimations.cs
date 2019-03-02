using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator headAC;
    [SerializeField]
    private Animator bodyAC;

    [HideInInspector]
    public bool isFighting = true;

    public void Shoot()
    {
        headAC.SetBool("isFighting", true);
        bodyAC.SetBool("isFighting", true);  
    }

    public void IdleAnimation()
    {
        headAC.SetBool("isFighting", false);
        bodyAC.SetBool("isFighting", false);      
    }
  


}
