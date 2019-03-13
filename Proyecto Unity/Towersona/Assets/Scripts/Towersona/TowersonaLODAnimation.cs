using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaLODAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator headAC;
    [SerializeField]
    private Animator bodyAC;

    [HideInInspector]
    public bool isFighting = true;

    public void Shoot()
    {
        //TODO: Towersona LOD animations
        //headAC.SetBool("isFighting", true);
        //bodyAC.SetBool("isFighting", true);  
    }

    public void IdleAnimation()
    {
        //TODO: Towersona LOD animations
        //headAC.SetBool("isFighting", false);
        //bodyAC.SetBool("isFighting", false);      
    }



}
