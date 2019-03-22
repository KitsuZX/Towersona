using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Shooting
{
    protected override void HitTarget()
    {
        Enemy e = target.GetComponent<Enemy>();

        Vector3 pos = transform.position;

        pos.y += 1f;   

        BuildManager.Instance.SpawnEffect(impactEffect, pos);

        CameraShake.Instance.AddTrauma(0.2f);

        if (e != null)
        {
            e.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
