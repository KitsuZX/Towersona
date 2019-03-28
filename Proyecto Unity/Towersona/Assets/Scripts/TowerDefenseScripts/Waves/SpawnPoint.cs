using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BezierSolution;

public class SpawnPoint : MonoBehaviour
{
    public BezierSpline path;
    public float timeBetweenGroups = 5f;
    public float timeBetweenStacks = 2.5f;
    public float timeBetweenEnemies = 1f;

    [Header("References")]
    [SerializeField]
    private GameObject[] enemyPrefab;
    [SerializeField]
    private Transform enemiesParent;

    public Wave[] waves;


    public IEnumerator SpawnWave(int waveIndex)
    {
        Wave wave = waves[waveIndex];

        if (wave.enemiesGroups == null) yield return null;        

        for (int i = 0; i < wave.enemiesGroups.Length; i++)
        {          
            yield return StartCoroutine(SpawnEnemiesGroup(wave.enemiesGroups[i]));             
        }

        print("Wave spawned");
    }

    IEnumerator SpawnEnemiesGroup(EnemiesGroup group)
    {
        for (int i = 0; i < group.stacks.Length; i++)
        {        
            yield return StartCoroutine(SpawnStack(group.stacks[i]));          
        }

        yield return new WaitForSeconds(timeBetweenGroups);

        print("Group spawned");      
    }

    IEnumerator SpawnStack(EnemyStack stack)
    {
        for (int i = 0; i < stack.numEnemies; i++)
        {
            SpawnEnemy(stack.enemyType);
            yield return new WaitForSeconds(timeBetweenEnemies);           
        }

        yield return new WaitForSeconds(timeBetweenStacks);

        print("Stack spawned");
     
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

        WavesController.Instance.enemiesAlive++;
    }   
}
