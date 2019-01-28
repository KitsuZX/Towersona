using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersController : MonoBehaviour
{ 
    [HideInInspector]
    public int towersBuilt = 0;
    [HideInInspector]
    public List<Towersona> towersonas;
    [HideInInspector]
    public List<TowersonaNeeds> towersonaNeeds;
    [HideInInspector]
    public Tile tileSelected;
    [HideInInspector]
    public bool towerAvaible = true;
    [HideInInspector]
    public bool maxReached = false;

    [Header("Parameters")]
    [SerializeField]
    private float timeBetweenTowersonas = 40f;
    [SerializeField]
    private int maxTowers = 5;
    [SerializeField]
    private Color[] colors;

    [Header("References")]
    [SerializeField]
    private GameObject towersonaPrefab;
    [SerializeField]
    private Text nextTowersonaText;
    [SerializeField]
    private GameObject detailedTowersonaViewPrefab;

    private GameManager gameManager;
    private World world;
    private float lastXUsed = 0f;
    private WorldGenerator worldGenerator;
    private WavesController wavesController;
    private float countdownTillNewTowersona;
    private Texture2D prevTexture; 
    private Stack<Color> towersonaColors;

    void Awake()
    {   
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        gameManager = GetComponent<GameManager>();
        wavesController = GetComponent<WavesController>();
       
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
        if (towersBuilt == maxTowers)
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
        towersBuilt++;
        Towersona towersona = Instantiate(towersonaPrefab, tile.transform.position, Quaternion.identity).GetComponent<Towersona>();
        towersona.tile = tile;
        towersona.ChangeColor();

        world.SelectTile(tile);
        towersonas.Add(towersona);
        towerAvaible = false;       
    }

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
