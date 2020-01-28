using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

	[Header("Level Number")]
	[SerializeField]
	private int levelNumber;

	[Header("Parameters")]
    public float timeTillNextWave;

	public int LevelNumber => levelNumber;
	public int LevelIndex => levelNumber - 1;


    [HideInInspector] public int enemiesAlive = 0;
    [HideInInspector] public int wavesToWin;
	[HideInInspector] public bool allEnemiesSpawned = true;

    private float countdown = 2f; 
    private Transform spawnPoint;
    private SpawnPoint[] spawnPoints;
    private int currentWave = 0;
	private bool WavesFinished { get
		{
			return currentWave == wavesToWin;
		}
	}

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);    
        spawnPoints = FindObjectsOfType<SpawnPoint>();

        wavesToWin = int.MinValue;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(spawnPoints[i].waves.Length > wavesToWin)
            {
                wavesToWin = spawnPoints[i].waves.Length;
            }
        }
    }

    private void Update()
    {
		//No empezar el timer si
		if(!allEnemiesSpawned ||							 //No se ha acabado de spawnear la oleada			
		   !DebuggingOptions.Instance.spawnEnemies ||        //No se quieren spawnear enemigos		
		   WavesFinished)										 
		{
			return;
		}				

        if(countdown <= 0f)
        {
			allEnemiesSpawned = false;
			StartCoroutine(SpawnWave());
            countdown = timeTillNextWave;			
            return;
        }		

		countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    private IEnumerator SpawnWave()
    {        
        PlayerStats.Instance.AddRound();

        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            yield return StartCoroutine(spawnPoint.SpawnWave(currentWave));
        }

        currentWave++;
		allEnemiesSpawned = true;
    }  

	public void EnemyDied()
	{
		enemiesAlive--;

		if(WavesFinished && enemiesAlive == 0 && allEnemiesSpawned)
		{
			GameManager.Instance.WinGame();
		}

	}
}

[System.Serializable]
public struct Wave
{
    [Header("Wave")]
    public List<EnemiesGroup> enemiesGroups;
    public float secondsToNextWave;
}

[System.Serializable]
public struct EnemiesGroup
{
    [Header("Group")]
    public int numEnemies;
    public float timeBetweenEnemies;
    public GameObject enemyType;
    public float secondsToNextGroup;

}
