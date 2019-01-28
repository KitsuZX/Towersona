using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Parameters")]
    [SerializeField]
    private int startingLifes = 10;

    [HideInInspector]
    public int round = 0;
    [HideInInspector]
    public int enemiesAlive = 0;

    [Header("References")]
    [SerializeField]
    private GameObject victoryPrompt;
    [SerializeField]
    private GameObject defeatPrompt;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text roundText;
    [SerializeField]
    private Camera cameraToDestroy;

    private int lifes;
    private World world;
    private WavesController wavesController;
    private TowersController towerDefenseManager;
    private Camera activeCamera;


    private void Awake()
    {
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
        roundText.text = "round: " + round.ToString() + "/" + wavesController.wavesToWin;
    }

    public void WinGame()
    {
        victoryPrompt.SetActive(true);
    }

    public void GameOver()
    {
        defeatPrompt.SetActive(true);
         CameraShake.Instance.AddTrauma(0.8f);
    }

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

    public void ChangeCamera(Towersona towersona)
    {
        activeCamera.enabled = false;

        if (cameraToDestroy != null)
        {
            Destroy(cameraToDestroy.gameObject);
        }

        activeCamera = towersona.towersonaNeedsCamera;

        activeCamera.enabled = true;
    }



}
