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
    [SerializeField][Tooltip("Colors to tint the Towersonas with")]
    private Color[] colors;

    [Header("References")]
    [SerializeField]
    private GameObject[] towersonaPrefabs;
    [SerializeField]
    private GameObject detailedTowersonaViewPrefab;

    //Private parameters
    private float lastXUsed = 0f;
    private Stack<Color> towersonaColors;
    private GameObject towersonaSelected;

    //Private references
    private GameManager gameManager;
    private World world;

    void Awake()
    {   
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        gameManager = GetComponent<GameManager>();
       
        towersonas = new List<Towersona>();

        towersonaColors = new Stack<Color>();
        foreach (Color color in colors)
        {
            towersonaColors.Push(color);
        }
    }

    public void SpawnTowersona(Tile tile)
    {
        if (towersonaSelected)
        {
            Towersona towersona = Instantiate(towersonaSelected, tile.transform.position, Quaternion.Euler(0f, 180f, 0f)).GetComponent<Towersona>();
            towersona.tile = tile;
            //towersona.ChangeColor();

            world.SelectTile(tile);
            towersonas.Add(towersona);
            towerAvaible = false;

            towersonaSelected = null;
        }
    }

    /// <summary>
    /// Creates a new Detailed Towersona view.
    /// </summary>
    /// <param name="towersona">Towersona whose scene is to be created</param>
    /// <returns>Reference to the new Detailed Towersona view</returns>
    public TowersonaNeeds SpawnDetailedTowersonaView(Towersona towersona)
    {        
        Vector3 position = Vector3.zero;
        position.x = lastXUsed;
        position.z = 50f;

        GameObject towersonaNeedsScene = Instantiate(detailedTowersonaViewPrefab, position, Quaternion.identity);
        TowersonaNeeds tsn = towersonaNeedsScene.GetComponentInChildren<TowersonaNeeds>();
        tsn.name = "Towersona need";

        lastXUsed += 15f;      

        SkinnedMeshRenderer[] smr = towersonaNeedsScene.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer m in smr)
        {
            //m.material.color = towersona.color;
        }

        towersonaNeeds.Add(tsn);

        return tsn;
    }

    public void SelectTowersona(int index)
    {
        towersonaSelected = towersonaPrefabs[index];
        Debug.Log("soy un puto boton");
    }

    public void DeselectTowersona()
    {
        towersonaSelected = null;
    }

    public Color GetColor()
    {
        return towersonaColors.Pop();
    } 
}
