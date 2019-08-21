using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersonaHODAnimation : MonoBehaviour
{
	public LookAwayFromTouch lookAway;

	[SerializeField] private LookAt lookAt = null; 
	
    private bool isLookingAtFood;
  
    public void Idle()
    {
		
    }

    public void Eat()
    {

    }

    public void CaressStart()
    {
		if(lookAway) lookAway.isBeingCaressed = true;
	}

    public void CaressEnd()
    {
		if (lookAway) lookAway.isBeingCaressed = false;
	}

    
    public void SetIsLookingAtFood(bool _isLookingAtFood)
    {
        isLookingAtFood = _isLookingAtFood;
        if(lookAt) lookAt.enabled = _isLookingAtFood;
    }

    public void SetLookAtTarget(Transform tr)
    {
        if(lookAt) lookAt.food = tr;
    }


}
