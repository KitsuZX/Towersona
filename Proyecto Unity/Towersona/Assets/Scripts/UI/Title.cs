using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    Vector3 pos;

    public float FloatStrength = 2f; // Set strength in Unity
    
    void Update()
    {
        pos = transform.position;    
        pos.y = (Mathf.Sin(Time.time) * FloatStrength);
        transform.position = pos;
    }
}
