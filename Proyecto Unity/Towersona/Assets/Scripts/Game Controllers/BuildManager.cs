using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{   
    public static BuildManager Instance { get; private set; }

	[Tooltip("El orden tiene que coincidir con el orden del Buy Menu")]	public Towersona[] towersonaPrefabs;

    [HideInInspector] public float lastXUsed = 0f;

    [Header("References")] 
    public GameObject detailedTowersonaViewPrefab;

	[SerializeField] private BuyMenu buyMenu = null;
	[SerializeField] private GameObject buildEffect = null;
    [SerializeField] private TowersonaConfirmation towersonaConfirmation;
    [SerializeField] private RangeShower rangeShower;

    //Private parameters  
    private List<Towersona> towersonas;
	private Towersona towersonaSelected;
	private BuildingPlace buildingPlaceSelected;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        towersonas = new List<Towersona>();       
    }

    private void Update()
    {       
		//Hides buyMenu if not clicked on it
		if (buyMenu.IsActive)
		{
			//https://answers.unity.com/questions/615771/how-to-check-if-click-mouse-on-object.html
			if (Input.GetMouseButtonDown(0))
			{
				PointerEventData pointerData = new PointerEventData(EventSystem.current);

				pointerData.position = Input.mousePosition;

				List<RaycastResult> results = new List<RaycastResult>();
				EventSystem.current.RaycastAll(pointerData, results);

				if (results.Count > 0)
				{
					//Check if another towersona was clicked
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

					if (Physics.Raycast(ray, out hit))
					{
						BuildingPlace bp = hit.transform.gameObject.GetComponent<BuildingPlace>();
						if (bp)
						{
							//A towersona was hit
							if (!bp.hasTower){

								ShowBuyMenu(bp);
							}
							else
							{
								Towersona t = hit.transform.gameObject.GetComponent<Towersona>();
								SelectTowersona(t);
							}
						}
						else
						{
							HideBuyMenu();
						}
					}
				}
				else
				{
					HideBuyMenu();
				}
			}
		}
	}

    public void SpawnTowersona(BuildingPlace place, Towersona _towersona)
    {
		place.hasTower = true;
        GameObject towersonaGameObject = Instantiate(_towersona.gameObject);
        towersonaGameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Towersonas Parent").transform, true);
        towersonaGameObject.name = _towersona.name;

        Towersona towersona = towersonaGameObject.GetComponent<Towersona>();
        towersona.Spawn(place, towersonaGameObject.transform);
		
        SpawnEffect(buildEffect, place.transform.position);
		PlayerStats.Instance.SpendMoney(towersona.stats.buyCost);

		towersonas.Add(towersona);       
    }     

    public void SetTowersonaConfirmation(BuildingPlace place, Towersona _towersona)
    {
        towersonaConfirmation.ActivateModel(place.buildingSpot.position, _towersona.towersonaLODPrefabs[0]);
        ShowRange(0, place, _towersona);
    }

	public void ShowRange(int level = 0, BuildingPlace place = null, Towersona towersona = null)
	{
		if (place != null && towersona != null && level == 0)
		{
			rangeShower.ShowRange(place.buildingSpot.position, towersona.stats);
		}
		else
		{
			rangeShower.ShowRange(towersonaSelected.place.buildingSpot.position, towersonaSelected.statsArray[level + 1]);
		}
	}

    public void DestroyTowersonaConfirmation()
    {
		if (towersonaConfirmation == null) return;
        towersonaConfirmation.DesactivateModel();
        rangeShower.HideRange();
    }

    public void ShowBuyMenu(BuildingPlace place)
	{
		if (buildingPlaceSelected == place)
		{
			return;
		}

		buildingPlaceSelected = place;
		buyMenu.SetPlace(place);		
	}

	public void HideBuyMenu()
	{
		buildingPlaceSelected = null;
		buyMenu.Hide();
		rangeShower.HideRange();
	}

    public void SelectTowersona(Towersona towersona)
    {
        towersonaSelected = towersona;
		buyMenu.SetPlace(towersona.place);	
    } 


    public void UpgradeTowersona(int level)
    {
        towersonaSelected.LevelUp(level);
        SpawnEffect(buildEffect, towersonaSelected.place.transform.position);

		HideBuyMenu();     
    }

    public void SellTowersona()
    {
		towersonaSelected.place.hasTower = false;
        towersonaSelected.Sell();
        towersonas.Remove(towersonaSelected);
		buildingPlaceSelected = null;
		HideBuyMenu();
        GameManager.Instance.ActivateEmptyCamera();                               
    }

    public void SpawnEffect(GameObject _effect, Vector3 position)
    {
        GameObject effect = Instantiate(_effect, position, Quaternion.identity);
        effect.transform.SetParent(GameObject.FindGameObjectWithTag("Effects Parent").transform, true);
        Destroy(effect, 5f);
    }      
}
