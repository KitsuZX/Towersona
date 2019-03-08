#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    [Header("Game Parameters")]
    public int wavesToWin = 10;
    [SerializeField][Tooltip("Starting lifes of the player")]
    private int startingLifes = 10;      

    /// <summary>
    /// Current round of the game
    /// </summary>
    [HideInInspector]
    public int round = 0;
    /// <summary>
    /// Number of enemies alive in the game
    /// </summary>
    [HideInInspector]
    public int enemiesAlive = 0;         

    [Header("Victory and Defeat References")]
    [SerializeField]
    private GameObject victoryPrompt;     
    [SerializeField]
    private GameObject defeatPrompt;      
    [Header("UI Texts References")]
    [SerializeField]
    private TextMeshProUGUI livesText;             
    [SerializeField]
    private TextMeshProUGUI roundText;
    [Header("Other References")]
    [SerializeField][Tooltip("The first detailed scene camera without the detailed model")]
    private Camera cameraToDestroy;

    //Private Parameters
    /// <summary>
    /// Current lifes of the player
    /// </summary>
    private int lifes;                   

    //Private references
    private World world;
    private WavesController wavesController;
    private TowersController towerDefenseManager;
    private Camera activeCamera;                    //Camera of the detailed scene of the Towersona selected

  
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        activeCamera = GameObject.FindGameObjectWithTag("Default Camera").GetComponent<Camera>();
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();

        wavesController = GetComponent<WavesController>();             
    }

    private void Start()
    {
        lifes = startingLifes;
        world.Generate();
    }

    private void Update()
    {
        livesText.text = "lives: " + lifes.ToString();
        roundText.text = "round: " + round.ToString() + "/" + wavesToWin;

        if(round == wavesToWin)
        {
            WinGame();
        }
    }
    
    /// <summary>
    /// Changes to victory state
    /// </summary>
    public void WinGame()
    {
        victoryPrompt.SetActive(true);
    }

    /// <summary>
    /// Changes to defeat state
    /// </summary>
    public void GameOver()
    {
        defeatPrompt.SetActive(true);
         CameraShake.Instance.AddTrauma(0.8f);
    }

    /// <summary>
    /// Takes out a life. If the number of lifes is less than 0, calls Game Over. Also adds Camera Shake.
    /// </summary>
    public void LoseLife()
    {
        lifes--;
        if (lifes <= 0)
        {
            GameOver();           

        }
        else
        {
            CameraShake.Instance.AddTrauma(0.5f);
        }
    }

    /// <summary>
    /// Changes the camera of the Detailed Scene to the Detailed Scene of a specific Towersona.
    /// </summary>
    /// <param name="towersona">Towersona to be shown in the Detailed Scene</param>
    public void ChangeCamera(Camera camera)
    {
        activeCamera.enabled = false;

        if (cameraToDestroy != null)
        {
            Destroy(cameraToDestroy.gameObject);
        }

        activeCamera = camera;

        activeCamera.enabled = true;
    }
}
