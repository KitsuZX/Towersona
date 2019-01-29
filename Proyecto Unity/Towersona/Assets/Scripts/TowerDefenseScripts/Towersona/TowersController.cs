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
    [SerializeField]
    private float timeBetweenTowersonas = 40f;
    [SerializeField]
    private int maxTowers = 5;
    [SerializeField][Tooltip("Colors to tint the Towersonas with")]
    private Color[] colors;

    [Header("References")]
    [SerializeField]
    private GameObject towersonaPrefab;
    [SerializeField]
    private Text nextTowersonaText;
    [SerializeField]
    private GameObject detailedTowersonaViewPrefab;

    //Private parameters
    private float lastXUsed = 0f;
    private float countdownTillNewTowersona;
    private Stack<Color> towersonaColors;

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

    private void Start() {
        countdownTillNewTowersona = timeBetweenTowersonas;       
    }  

    private void Update()
    {       
        //Towersona building
        if (towersonas.Count == maxTowers)
        {
            maxReached = true;
            nextTowersonaText.text = "no more towersonas avaible!";
            return;
        }

        if (!towerAvaible)
        {
            countdownTillNewTowersona -= Time.deltaTime;
            if (countdownTillNewTowersona <= 0f)
            {
                towerAvaible = true;
                countdownTillNewTowersona = timeBetweenTowersonas;
            }

            nextTowersonaText.text = "new towersona in: " + Mathf.Floor(countdownTillNewTowersona + 1);
        }
        else
        {
            nextTowersonaText.text = "towesona avaible!";
        }       
        
    }

    public void SpawnTowersona(Tile tile)
    {   
        Towersona towersona = Instantiate(towersonaPrefab, tile.transform.position, Quaternion.identity).GetComponent<Towersona>();
        towersona.tile = tile;
        towersona.ChangeColor();

        world.SelectTile(tile);
        towersonas.Add(towersona);
        towerAvaible = false;       
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
            m.material.color = towersona.color;
        }

        towersonaNeeds.Add(tsn);

        return tsn;
    }

    public Color GetColor()
    {
        return towersonaColors.Pop();
    } 
}
