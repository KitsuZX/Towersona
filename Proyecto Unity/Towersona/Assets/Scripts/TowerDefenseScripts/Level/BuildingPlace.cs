using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlace : MonoBehaviour
{	
    public bool hasTower = false;
	public Transform buildingSpot;
	
    private void OnMouseUpAsButton()
    {
		if (!hasTower)
		{
			BuildManager.Instance.ShowBuyMenu(this);
		}
	}
}
