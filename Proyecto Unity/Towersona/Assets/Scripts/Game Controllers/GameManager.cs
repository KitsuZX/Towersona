#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    
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

    [Header("Other References")]
    [SerializeField][Tooltip("The first detailed scene camera without the detailed model")]
    private Camera cameraToDestroy;

    //Private Parameters             

    //Private references
    private World world;
    private WavesController wavesController;
    private BuildManager towerDefenseManager;
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
        //world.Generate();
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
