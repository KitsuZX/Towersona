using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [HideInInspector]
    public Transform food;
    
    private new Transform transform;

    private void LateUpdate()
    {
        if (food == null) return;
        //Vector3 diff = food.position - transform.position;
        //transform.rotation =  Quaternion.LookRotation(diff, Vector3.up);

        transform.LookAt(food);
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

}
