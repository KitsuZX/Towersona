using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BezierSolution;
using UnityEditor;

public class SpawnPoint : MonoBehaviour
{
    public BezierSpline path;
    
    public Wave[] waves;

    [Header("References")]
    [SerializeField]
    private GameObject[] enemiesPrefab;

    private Transform enemiesParent;


    private void Awake()
    {
        enemiesParent = GameObject.FindGameObjectWithTag("Enemies Parent").transform;
    }

    public IEnumerator SpawnWave(int waveIndex)
    {
        Wave wave = waves[waveIndex];

        if (wave.enemiesGroups == null) yield return null;        

        for (int i = 0; i < wave.enemiesGroups.Count; i++)
        {          
            yield return StartCoroutine(SpawnEnemiesGroup(wave.enemiesGroups[i]));             
        }  
    }

    IEnumerator SpawnEnemiesGroup(EnemiesGroup group)
    {
        for (int i = 0; i < group.numEnemies; i++)
        {
            SpawnEnemy(group.enemyType);
            yield return new WaitForSeconds(group.timeBetweenEnemies);                     
        }

        yield return new WaitForSeconds(group.secondsToNextGroup);          
    }   

    void SpawnEnemy(GameObject enemyType)
    {        
        GameObject enemyObject = Instantiate(enemyType, transform.position, Quaternion.Euler(0f, 90f, 0f));
        enemyObject.transform.SetParent(enemiesParent);

        enemyObject.transform.position += new Vector3(0f, 0f, Random.Range(-1f, 1f));
        Enemy enemy = enemyObject.GetComponent<Enemy>();

        UnityAction action = enemy.EndPath;

        BezierWalkerWithSpeed bezierWalker = enemyObject.AddComponent<BezierWalkerWithSpeed>();

        bezierWalker.spline = path;
        bezierWalker.onPathCompleted.AddListener(action);
        bezierWalker.speed = 3;

        LevelManager.Instance.enemiesAlive++;
    }   
}
