using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Parameters")]
    public float timeBetweenWaves;

    [HideInInspector]
    public int enemiesAlive = 0;
    [HideInInspector]
    public int wavesToWin = 10;

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
        if (!DebuggingOptions.Instance.spawnEnemies) return;

        if(enemiesAlive > 0)
        {
            return;
        }
  
        if(countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    private void SpawnWave()
    {
        PlayerStats.Instance.AddRound();      
            
        int enemiesToSpawn = PlayerStats.Instance.round * 4;

        foreach(SpawnPoint spawnPoint in spawnPoints)
        {
           StartCoroutine(spawnPoint.SpawnWave(waveIndex));
        }

        waveIndex++;
         
    }  
}

[System.Serializable]
public struct Wave
{
    public List<EnemiesGroup> enemiesGroups;
}

[System.Serializable]
public struct EnemiesGroup
{    
    public int numEnemies;
    public float timeBetweenEnemies;
    public GameObject enemyType;
    public float secondsToNextGroup;

}