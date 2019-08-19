using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Parameters")]
    public float timeTillNextWave;

    [HideInInspector]
    public int enemiesAlive = 0;
    [HideInInspector]
    public int wavesToWin;
    [HideInInspector]
    public bool spawningWave = false;

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

        if(spawningWave)
        {
            return;
        }
  
        if(countdown <= 0f && !GameManager.Instance.gameOver)
        {
            SpawnWave();
            countdown = timeTillNextWave;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    private void SpawnWave()
    {
        spawningWave = true;
        PlayerStats.Instance.AddRound();

        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            StartCoroutine(spawnPoint.SpawnWave(waveIndex));
        }

        waveIndex++;         
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
