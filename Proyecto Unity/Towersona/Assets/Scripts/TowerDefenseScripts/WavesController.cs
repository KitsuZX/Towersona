using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public static int EnemiesAlives = 0;
    public static WavesController Instance { get; private set; }

    public int wavesToWin = 10;
    public float timeBetweenWaves;
    public float waveSpawnRate = 1;

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform enemiesParent;

    private float countdown = 2f; 
    private Transform spawnPoint;


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
    }

    private void Update()
    {        
        if(EnemiesAlives > 0)
        {
            return;
        }

        if(PlayerStats.Rounds == wavesToWin)
        {
            TowerDefenseManager.Instance.WinGame();
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
        PlayerStats.Rounds++;      
            
        int enemiesToSpawn = PlayerStats.Rounds * 3;

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
        EnemiesAlives++;
    }

    public void SetSpawnPoint(Transform transform)
    {
        spawnPoint = transform;
        spawnPoint.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

}
