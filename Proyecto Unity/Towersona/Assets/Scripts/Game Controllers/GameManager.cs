#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelManager), typeof(PlayerStats))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    

    [Header("Other References")]
    [SerializeField, Tooltip("The first detailed scene camera without the detailed model")]
    private Camera emptyCamera;

    public event System.Action OnWonGame;

    //Private references
    private LevelManager wavesController;
    private PlayerStats playerStats;
    private BuildManager towerDefenseManager;
    private Camera activeCamera;                    //Camera of the detailed scene of the Towersona selected
  
    private void Awake()
    {
		Time.timeScale = 1;

        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        activeCamera = GameObject.FindGameObjectWithTag("Default Camera").GetComponent<Camera>();     

        wavesController = GetComponent<LevelManager>();        
        playerStats = GetComponent<PlayerStats>();

		if(SaveSystem.levelsData[0] == null)
		{
			SaveSystem.LoadLevelsData();
		}
	}

	private void Start()
	{
		WinGame();
	}

	/// <summary>
	/// Changes to victory state
	/// </summary>
	public void WinGame()
    {
		StartCoroutine(InGameUIController.Instance.ShowVictoryPrompt(playerStats.Score));

        OnWonGame?.Invoke();    //Lo invoco así porque si no hay listeners el evento es null.
    }

    /// <summary>
    /// Changes to defeat state
    /// </summary>
    public void GameOver()
    {
		InGameUIController.Instance.ShowDefeatPrompt();
	}

    /// <summary>
    /// Changes the camera of the Detailed Scene to the Detailed Scene of a specific Towersona.
    /// </summary>
    public void ChangeCamera(Camera camera)
    {     
        if (emptyCamera.gameObject.activeSelf)
        {
            emptyCamera.gameObject.SetActive(false);
        }
        else
        {
            activeCamera.enabled = false;
        }

        activeCamera = camera;

        activeCamera.enabled = true;
    }

    public void ActivateEmptyCamera()
    {
        emptyCamera.gameObject.SetActive(true);
    }
}
