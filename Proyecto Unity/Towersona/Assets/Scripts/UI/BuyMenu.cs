using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyMenu : MonoBehaviour
{
	private BuildingPlace place;
	[SerializeField] private TextMeshProUGUI[] texts = null;

	private void Awake()
	{
		for (int i = 0; i < texts.Length; i++)
		{
			Towersona t = BuildManager.Instance.towersonaPrefabs[i];
			if(t == null)
			{
				Debug.LogError("No se ha encontrado la towersona. ¿Están en el orden correcto en BuildManager y en BuyMenu?");
			}
			else
			{
				texts[i].text = t.menuName + " " + t.stats.buyCost + "$";
			}			
		}
	}

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
