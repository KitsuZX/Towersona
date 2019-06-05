using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : MonoBehaviour
{

	private BuildingPlace place;

    public void SetPlace(BuildingPlace _place)
	{
		place = _place;
		Vector3 pos = _place.transform.position;
		pos.y += 1f;
		pos.z += 1f;

		transform.position = pos;
		
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void BuyTowersona(Towersona towersona)
	{	
		BuildManager.Instance.SpawnTowersona(place, towersona);
		Hide();
	}

	
}
