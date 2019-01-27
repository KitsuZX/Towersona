using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float life = 30f;
    [SerializeField]
    private GameObject deathEffect;

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
        KillEnemy(true);
        PlayerStats.LoseLife();
        
    }

    private void KillEnemy(bool endPath = false)
    {
        CameraShake.Instance.AddTrauma(0.4f);
        Vector3 pos = transform.position;
        pos.y += 0.5f;
        if (!endPath)
        {
            GameObject effect = Instantiate(deathEffect, pos, Quaternion.identity);
            Destroy(effect, 5f);
        }

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
