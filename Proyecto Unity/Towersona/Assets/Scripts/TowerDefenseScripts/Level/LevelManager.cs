using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Parameters")]
    public float timeTillNextWave;

    [HideInInspector] public int enemiesAlive = 0;
    [HideInInspector] public int wavesToWin;
	[HideInInspector] public bool allEnemiesSpawned = true;

    private float countdown = 2f; 
    private Transform spawnPoint;
    private GameManager gameManager;
    private SpawnPoint[] spawnPoints;
    private int waveIndex = 0;

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
			!DebuggingOptions.Instance.spawnEnemies)		 //No se quieen spawnear enemigos					
		{
			return;
		}

		/*if (allEnemiesSpawned && enemiesAlive <= 0)
		{
			if(countdown > 2f)
			{
				countdown = 2f;
			}
		}*/

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
            yield return StartCoroutine(spawnPoint.SpawnWave(waveIndex));
        }

        waveIndex++;
		allEnemiesSpawned = true;
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
