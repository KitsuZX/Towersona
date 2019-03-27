using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [Header("Parameters")]
    public float timeBetweenWaves;
    public float waveSpawnRate = 1;

    private float countdown = 2f; 
    private Transform spawnPoint;
    private GameManager gameManager;
    private SpawnPoint[] spawnPoints;
    private int waveIndex = 0;


    private void Awake()
    {     
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.enemiesAlive = 0;
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    private void Update()
    {
        if (!DebuggingOptions.Instance.spawnEnemies) return;

        if(gameManager.enemiesAlive > 0)
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
