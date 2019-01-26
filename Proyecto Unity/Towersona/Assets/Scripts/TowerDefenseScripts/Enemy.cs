using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float life = 30f;

    private Transform target;
    private int controlPointIndex = 0;     

    private void Awake()
    {
        target = World.Instance.controlPoints[0].transform;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if(controlPointIndex >= World.Instance.controlPoints.Count - 1)
        {
            EndPath();
            return;
        }

        controlPointIndex++;
        target = World.Instance.controlPoints[controlPointIndex];
        transform.LookAt(target);
    }

    private void EndPath()
    {
        KillEnemy();
        PlayerStats.LoseLife();
        
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
        WavesController.EnemiesAlives--;
    }

    public void TakeDamage(float amount) {
        life -= amount;
        if(life <= 0)
        {
            KillEnemy();
        }
    }


}
