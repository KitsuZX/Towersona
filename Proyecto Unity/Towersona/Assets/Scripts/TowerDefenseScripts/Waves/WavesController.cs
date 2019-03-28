using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public static WavesController Instance { get; private set; }

    [Header("Parameters")]
    public float timeBetweenWaves;
    public float waveSpawnRate = 1;

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

    public void SetSpawnPoint(Transform transform)
    {
        spawnPoint = transform;
        spawnPoint.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

}

[System.Serializable]
public struct Wave
{
    public EnemiesGroup[] enemiesGroups;
}

[System.Serializable]
public struct EnemiesGroup
{
    public EnemyStack[] stacks;

}

[System.Serializable]
public struct EnemyStack
{
    public int numEnemies;
    public GameObject enemyType;
}
