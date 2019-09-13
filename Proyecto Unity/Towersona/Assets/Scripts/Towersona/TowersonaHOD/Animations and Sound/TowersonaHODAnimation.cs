using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersonaHODAnimation : MonoBehaviour
{
	public LookAwayFromTouch lookAway;

	[SerializeField] private LookAt lookAt = null;
	[SerializeField] private Animator bodyAnimator = null;
	[SerializeField] private TowersonaLOD towersonaLOD = null;

    private bool isLookingAtFood;
	private Animator lodBodyAnimator;

	private void Start()
	{
		lodBodyAnimator = towersonaLOD.GetComponent<TowersonaLODAnimation>().bodyAnimator;
	}

	public void Eat()
	{

	}
  
    public void SetLoneliness(bool loneliness)
	{
		//TODO: evitar que esto se esté llamando todo el rato
		/*bodyAnimator.SetBool("isLonely", loneliness);
		lodBodyAnimator.SetBool("isLonely", loneliness);*/
	}

	public void TakeAShit()
	{
		//bodyAnimator.SetTrigger("takeADump");
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
