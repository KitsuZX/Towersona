using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Towersonas/Hedgehog")]
public class HedgehogStats : TowersonaStats
{
	[Header("Damage")]
    public Vector2 bulletDamage;
    public Vector2 attackSpeed;
    public Vector2 range;
    public Vector2 bulletSpeed;

    public override void UpdateStats()
    {       
        currentAttackStrength = Mathf.Lerp(bulletDamage.x, bulletDamage.y, needs.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
        currentAttackRange = Mathf.Lerp(range.x, range.y, needs.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);		
	}

    public override void SetDefaultValues()
    {
        currentAttackStrength = bulletDamage.y;
        currentAttackSpeed = attackSpeed.y;
        currentAttackRange = range.y;
        currentBulletSpeed = bulletSpeed.y;  
    }
}
