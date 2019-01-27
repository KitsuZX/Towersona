using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtFood : MonoBehaviour
{
    public Transform food;

    private new Animator animator;






    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

}
