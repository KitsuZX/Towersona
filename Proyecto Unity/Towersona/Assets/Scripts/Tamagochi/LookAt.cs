using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [HideInInspector]
    public Transform food;

    private new Animator animator;
    private new Transform transform;

    private void LateUpdate()
    {
        Vector3 diff = food.position - transform.position;
        transform.rotation =  Quaternion.LookRotation(diff, Vector3.up);
    }



    private void Awake()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

}
