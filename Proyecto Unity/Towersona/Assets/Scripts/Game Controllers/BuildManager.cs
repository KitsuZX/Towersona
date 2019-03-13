using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{   
    [HideInInspector]
    public List<Towersona> towersonas;
    [HideInInspector]
    public List<TowersonaNeeds> towersonaNeeds;
    [HideInInspector]
    public bool towerAvaible = true;
    /// <summary>
    /// Max number of Towersonas has been reached
    /// </summary>s
    [HideInInspector]
    public bool maxReached = false;

    [Header("Parameters")]  
    public float timeBetweenTowersonas = 40f;

    [Header("References")]
    [SerializeField]
    private GameObject[] towersonaPrefabs;
    [SerializeField]
    private GameObject detailedTowersonaViewPrefab;
    [SerializeField]
    private NodeUI nodeUI;
    [SerializeField]
    private GameObject buildEffect;

    //Private parameters
    private float lastXUsed = 0f;
    private Stack<Color> towersonaColors;
    private GameObject towersonaToBuild;
    private Towersona towersonaSelected;

    //Private references
    private GameManager gameManager;
    private World world;

    void Awake()
    {   
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        gameManager = GetComponent<GameManager>();
       
        towersonas = new List<Towersona>();

        towersonaColors = new Stack<Color>();      
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

                if (results.Count <= 0)               
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
            }
        }
    }

    public void SpawnTowersona(Tile tile)
    {
        if (towersonaToBuild)
        {
         
            Towersona towersona = Instantiate(towersonaToBuild, tile.transform.position, Quaternion.Euler(0f, 180f, 0f)).GetComponent<Towersona>();   
            towersona.tile = tile;
            //towersona.ChangeColor();

            world.SelectTile(tile);
            towersonas.Add(towersona);
            towerAvaible = false;

            SpawnEffect(buildEffect, towersona.transform.position);


            towersonaToBuild = null;
        }
    }

    /// <summary>
    /// Creates a new Detailed Towersona view.
    /// </summary>
    /// <param name="towersona">Towersona whose scene is to be created</param>
    /// <returns>Reference to the new Detailed Towersona view</returns>
    public DetailedTowersonaView SpawnDetailedTowersonaView(Towersona towersona)
    {        
        Vector3 position = Vector3.zero;
        position.x = lastXUsed;
        position.z = 50f;

        GameObject towersonaNeedsScene = Instantiate(detailedTowersonaViewPrefab, position, Quaternion.identity);
        DetailedTowersonaView detailedTowersonaView = towersonaNeedsScene.GetComponent<DetailedTowersonaView>();
        Camera detailedTowersonaCamera = detailedTowersonaView.transform.GetComponentInChildren<Camera>();
        gameManager.ChangeCamera(detailedTowersonaCamera);

        TowersonaNeeds tsn = detailedTowersonaView.SpawnTowersonaNeeds(towersona);
        tsn.name = "Towersona need";

        lastXUsed += 15f;      
  
        return detailedTowersonaView;
    }

    public void SelectTowersonaToBuild(Towersona _towersona)
    {
        towersonaToBuild = _towersona.gameObject;

        DeselectTowersona();
    }

    public void SelectTowersona(Towersona towersona)
    {
        if(towersonaSelected == towersona)
        {            
            return;
        }

        towersonaSelected = towersona;
        towersonaToBuild = null;
         
        nodeUI.SetTarget(towersona);
    }

    public void DeselectTowersona()
    {
        towersonaSelected = null;
        nodeUI.Hide();
    }   

    public void UpgradeTowersona()
    {
        towersonaSelected.Upgrade();
        SpawnEffect(buildEffect, towersonaSelected.transform.position);

        DeselectTowersona();     
    }

    public void Evolve(int evolution)
    {
        towersonaSelected.Evolve(evolution);
        SpawnEffect(buildEffect, towersonaSelected.transform.position);

        DeselectTowersona();
    }

    private void SpawnEffect(GameObject _effect, Vector3 position)
    {
        GameObject effect = Instantiate(_effect, position, Quaternion.identity);
        Destroy(effect, 5f);
    }
}
