using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDefenseManager : MonoBehaviour
{
    public static TowerDefenseManager Instance { get; private set; }

    [HideInInspector]
    public int towersBuilt = 0;
    [HideInInspector]
    public List<Towersona> towersonas;
    [HideInInspector]
    public Tile tileSelected;

    [SerializeField]
    private float timeBetweenTowersonas = 5f;

    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text roundText;
    [SerializeField]
    private Text nextTowersonaText;
    [SerializeField]
    private int maxTowers = 5;

    private WorldGenerator worldGenerator;
    private WavesController wavesController;
    private float countdownTillNewTowersona;

    private Texture2D prevTexture;   


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

        worldGenerator = GetComponent<WorldGenerator>();
        wavesController = GetComponent<WavesController>();
        towersonas = new List<Towersona>();
    }

    private void Start() {
        countdownTillNewTowersona = timeBetweenTowersonas;

        worldGenerator.GenerateWorld();
        worldGenerator.GeneratePath();
    }

    public void GameOver()
    {
        Debug.Log("PERDISTE! LAS TOWERSONAS HAN MUERTO!");
    }

    public void WinGame()
    {
        Debug.Log("GANASTE GORDO PENDEJO");
    }

    private void Update()
    {
        livesText.text = "lives: " + PlayerStats.Lives.ToString();
        roundText.text = "round: " + PlayerStats.Rounds.ToString();

        //Towersona building
        if (towersBuilt == maxTowers)
        {
            PlayerStats.MaxReached = true;
            nextTowersonaText.text = "no more towersonas avaible!";
            return;
        }

        if (!PlayerStats.TowerAvaible)
        {
            countdownTillNewTowersona -= Time.deltaTime;
            if (countdownTillNewTowersona <= 0f)
            {
                PlayerStats.TowerAvaible = true;
                countdownTillNewTowersona = timeBetweenTowersonas;
            }

            nextTowersonaText.text = "new towersona in: " + Mathf.Floor(countdownTillNewTowersona + 1);
        }
        else
        {
            nextTowersonaText.text = "towesona avaible!";
        }       
        
    }   

    public void SelectTile(Tile tile)
    {
        if(tileSelected != null)
        {           
            tile.DeselectTile();
        }

        tileSelected = tile;
        tile.SelectTile();
    }
}
