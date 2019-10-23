using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [HideInInspector]
    public Transform food;
    
    private new Transform transform;
	private Vector3 initialRotation;

	private void Awake()
	{
		transform = GetComponent<Transform>();
		initialRotation = transform.eulerAngles;
		initialRotation.x -= 180;
		initialRotation.z -= 180;
	}

	private void LateUpdate()
    {
        if (food == null) return;        

        transform.LookAt(food);
		transform.eulerAngles += initialRotation;
    } 
}
