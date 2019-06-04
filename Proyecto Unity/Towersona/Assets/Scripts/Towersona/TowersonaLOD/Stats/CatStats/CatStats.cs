using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Cat/Cat")]
public class CatStats : TowersonaStats
{
	[Header("Damage")]
    public Vector2 bulletDamage;
    public Vector2 attackSpeed;
    public Vector2 range;
    public Vector2 bulletSpeed;

	[Header("Money")]
	public Vector2 timeSpanBetweenGivingMoney;
    public Vector2 moneyGiven;
    
    [HideInInspector]
	public float currentTimeSpan;
	[HideInInspector]
	public int currentMoneyGiven;

    public override void UpdateStats()
    {       
        currentAttackStrength = Mathf.Lerp(bulletDamage.x, bulletDamage.y, needs.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
        currentAttackRange = Mathf.Lerp(range.x, range.y, needs.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);

		currentTimeSpan = Mathf.Lerp(timeSpanBetweenGivingMoney.x, timeSpanBetweenGivingMoney.y, needs.HappinessLevel);
		currentMoneyGiven = (int) Mathf.Lerp(moneyGiven.x, moneyGiven.y, needs.HappinessLevel);
	}

    public override void SetDefaultValues()
    {
        currentAttackStrength = bulletDamage.y;
        currentAttackSpeed = attackSpeed.y;
        currentAttackRange = range.y;
        currentBulletSpeed = bulletSpeed.y;

        currentMoneyGiven = (int) moneyGiven.y;
    }
}
