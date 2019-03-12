using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersController : MonoBehaviour
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
    private Towersona[] towersonaPrefabs;
    [SerializeField]
    private GameObject detailedTowersonaViewPrefab;

    //Private parameters
    private float lastXUsed = 0f;
    private Stack<Color> towersonaColors;
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

    public void SpawnTowersona(Tile tile)
    {
        if (towersonaSelected)
        {
            /*Towersona towersona = Instantiate(towersonaSelected, tile.transform.position, Quaternion.Euler(0f, 180f, 0f)).GetComponent<Towersona>();   
            towersona.tile = tile;   */

            towersonaSelected.Spawn(tile);

            world.SelectTile(tile);
            towersonas.Add(towersonaSelected);

            towerAvaible = false;
            towersonaSelected = null;
        }
    }

    /// <summary>
    /// Creates a new Detailed Towersona view.
    /// </summary>
    /// <param name="towersona">Towersona whose scene is to be created</param>
    /// <returns>Reference to the new Detailed Towersona view</returns>
    public GameObject SpawnDetailedTowersonaView(Towersona towersona)
    {        
        Vector3 position = Vector3.zero;
        position.x = lastXUsed;
        position.z = 50f;

        GameObject towersonaNeedsScene = Instantiate(detailedTowersonaViewPrefab, position, Quaternion.identity);
        DetailedTowersonaView detailedTowersonaView = towersonaNeedsScene.GetComponent<DetailedTowersonaView>();
        Camera detailedTowersonaCamera = detailedTowersonaView.transform.GetComponentInChildren<Camera>();
        gameManager.ChangeCamera(detailedTowersonaCamera);

        GameObject towerModel = detailedTowersonaView.SpawnTowersonaNeedsModel(towersona.highpolyModels[0], towersona);
        towerModel.name = "Towersona need";

        lastXUsed += 15f;      
  
        return towerModel;
    }

    public void SelectTowersona(Towersona towersona)
    {
        towersonaSelected = towersona;
    }

    public void DeselectTowersona()
    {
        towersonaSelected = null;
    }
}
