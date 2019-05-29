using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersonaHODAnimation : MonoBehaviour
{
	public LookAwayFromTouch lookAway;
	[SerializeField]
	private LookAt lookAt;

	[SerializeField]
    private Animator bodyAnimator;
    [SerializeField]
    private Animator faceAnimator; 
	
    private bool isLookingAtFood;

  
    public void Idle()
    {

    }

    public void Eat()
    {

    }

    public void CaressStart()
    {

    }

    public void CaressEnd()
    {

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
