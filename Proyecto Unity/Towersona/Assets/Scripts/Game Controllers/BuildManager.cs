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
	

    [SerializeField] private NodeUI nodeUI = null;
	[SerializeField] private BuyMenu buyMenu = null;
	[SerializeField] private GameObject buildEffect = null;

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
        //Hides nodeUI if not clicked on it
        if (nodeUI.UIIsActive)
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
                        Towersona t = hit.transform.gameObject.GetComponent<Towersona>();
                        if (t)
                        {
                            //A towersona was hit
                            SelectTowersona(t);
                        }
                        else
                        {
                            DeselectTowersona();
                        }                     
                    }
				}
				else
				{
					DeselectTowersona();
				}
            }		
        }

		//Hides buyMenu if not clicked on it
		if (buyMenu.gameObject.activeSelf)
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
								HideBuyMenu();
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

	public void ShowBuyMenu(BuildingPlace place)
	{
		if (buildingPlaceSelected == place)
		{
			return;
		}

		buildingPlaceSelected = place;
		buyMenu.SetPlace(place);

		if (nodeUI.UIIsActive)
		{
			nodeUI.Hide();
		}
	}

	public void HideBuyMenu()
	{
		buildingPlaceSelected = null;
		buyMenu.Hide();
	}

    public void SelectTowersona(Towersona towersona)
    {
        if(towersonaSelected == towersona)
        {            
            //return;
        }

        towersonaSelected = towersona;
         
        nodeUI.SetTarget(towersona);

		if (buyMenu.gameObject.activeSelf)
		{
			buyMenu.Hide();
		}
    }

    public void DeselectTowersona()
    {       
        nodeUI.Hide();
    }   

    public void UpgradeTowersona(int level)
    {
        towersonaSelected.LevelUp(level);
        SpawnEffect(buildEffect, towersonaSelected.place.transform.position);

        DeselectTowersona();     
    }

    public void SellTowersona()
    {
		towersonaSelected.place.hasTower = false;
        towersonaSelected.Sell();
        towersonas.Remove(towersonaSelected);
		buildingPlaceSelected = null;
		DeselectTowersona();
        GameManager.Instance.ActivateEmptyCamera();                               
    }

    public void SpawnEffect(GameObject _effect, Vector3 position)
    {
        GameObject effect = Instantiate(_effect, position, Quaternion.identity);
        effect.transform.SetParent(GameObject.FindGameObjectWithTag("Effects Parent").transform, true);
        Destroy(effect, 5f);
    }   

    private void OnGUI()
    {       
        if (towersonaSelected != null)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 32;
            style.fontStyle = FontStyle.Bold;

            string message = "";
            message += "Fuerza: " + towersonaSelected.stats.AttackStrength + "\n";
            message += "V. Ataque: " + towersonaSelected.stats.AttackSpeed + "\n";
            message += "Rango: " + towersonaSelected.stats.currentAttackRange + "\n";
            message += "V. Bala: " + towersonaSelected.stats.currentBulletSpeed + "\n";

            GUI.Label(new Rect(Screen.width * 0.66f + 110, 200, 120, 100), message, style);            
        }
    }
}
