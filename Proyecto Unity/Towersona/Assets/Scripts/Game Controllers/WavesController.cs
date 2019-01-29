using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    [Header("Parameters")]
    public float timeBetweenWaves;
    public float waveSpawnRate = 1;

    [Header("References")]
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform enemiesParent;

    private float countdown = 2f; 
    private Transform spawnPoint;
    private GameManager gameManager;


    private void Awake()
    {     
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.enemiesAlive = 0;
    }

    private void Update()
    {        
        if(gameManager.enemiesAlive > 0)
        {
            return;
        }
  
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    private IEnumerator SpawnWave()
    {
        gameManager.round++;      
            
        int enemiesToSpawn = gameManager.round * 3;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(1f / waveSpawnRate);
        }       
    }  

    void SpawnEnemy(GameObject enemy)
    {
        GameObject e = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        e.transform.SetParent(enemiesParent);
        gameManager.enemiesAlive++;
    }

    public void SetSpawnPoint(Transform transform)
    {
        spawnPoint = transform;
        spawnPoint.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

}
