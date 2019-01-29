using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularAttack : AttackPattern
{
  
    public override void Shoot(Transform target)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.damage = attackStrength;
        bullet.speed = bulletSpeed;

        if (bullet != null)
            bullet.Seek(target);
    }

}
