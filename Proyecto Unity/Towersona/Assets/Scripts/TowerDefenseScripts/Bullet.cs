using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
 
    public float speed = 10f;
 
    [HideInInspector]
    public float damage;

    [SerializeField]
    private GameObject impactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Enemy e = target.GetComponent<Enemy>();

        Vector3 pos = transform.position;

        pos.y += 1f;

        GameObject effectIns = Instantiate(impactEffect, pos, transform.rotation);
        Destroy(effectIns, 2f);

        CameraShake.Instance.AddTrauma(0.2f);

        if (e != null)
        {
            e.TakeDamage(damage);
        }


        Destroy(gameObject);
    }
}
