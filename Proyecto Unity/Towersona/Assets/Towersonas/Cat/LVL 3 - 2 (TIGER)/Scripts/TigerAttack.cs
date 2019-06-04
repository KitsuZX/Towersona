using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAttack : CatAttack
{
	TigerStats tigerStats;
	private void Start()
	{
		base.Start();
		tigerStats = (TigerStats)stats;	
	}

	public override void Shoot(Transform target)
	{
		GameObject bulletObject = Instantiate(bulletPrefab, towersonaLOD.firePoint.position, towersonaLOD.firePoint.rotation);
		bulletObject.transform.SetParent(GameObject.FindGameObjectWithTag("Bullets Parent").transform, true);

		TigerBullet bullet = bulletObject.GetComponent<TigerBullet>();
		bullet.damage = tigerStats.currentAttackStrength;
		bullet.speed = tigerStats.currentBulletSpeed;
		bullet.explosionRadius = tigerStats.currentDamageArea;

		if (bullet != null) bullet.Seek(target);
	}	
}
