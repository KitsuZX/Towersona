using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Feedable : MonoBehaviour
{
    public event Action<Food> OnFed;

    public void Feed(Food food)
    {
        OnFed?.Invoke(food);
    }


    private void Start()
    {
        Debug.Assert(gameObject.layer == LayerMask.NameToLayer("FoodLayer"), 
            "Feedable components must be in layer FoodLayer", this);
    }
}
