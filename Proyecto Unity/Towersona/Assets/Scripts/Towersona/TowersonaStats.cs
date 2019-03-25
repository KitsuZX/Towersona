using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaStats
{
    public Vector2 attackStrength;
    public Vector2 attackSpeed;
    public Vector2 attackRange;
    public Vector2 bulletSpeed;
    public float hungerDecayPerSecond;
    public float loveDecayPerSecond;
    public float loveIncreasePerDeltaUnit;
    public float happinessImpactPerShit;
    public float shittingInterval;
    public int maxShitCount;

    public float currentAttackStrength;
    public float currentAttackSpeed;
    public float currentAttackRange;
    public float currentBulletSpeed;

    Towersona towersona;
    TowersonaNeeds needs;

    public TowersonaStats(Towersona towersona)
    {
        this.towersona = towersona;
        needs = towersona.towersonaNeeds;

        AssignData();      
    }

    public void AssignData()
    {
        List<float> data = CSVReader.ReadTowersonaCSV("Cat Towersona Stats.csv", (int)towersona.towersonaLevel);

        if (data == null)     
        {
            Debug.LogError("Ha habido problemas al leer los datos del csv");
            return;
        }

        attackStrength.x = data[0];
        attackStrength.y = data[1];

        attackSpeed.x = data[2];
        attackSpeed.y = data[3];

        attackRange.x = data[4];
        attackRange.y = data[5];

        bulletSpeed.x = data[6];
        bulletSpeed.y = data[7];

        hungerDecayPerSecond = data[8];
        loveDecayPerSecond = data[9];
        loveIncreasePerDeltaUnit = data[10];
        happinessImpactPerShit = data[11];
        shittingInterval = data[12];
        maxShitCount = (int)data[13];

        SetDefaultValues();
    }

    public void UpdateStats()
    {
        currentAttackStrength = Mathf.Lerp(attackStrength.x, attackStrength.y, needs.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(attackSpeed.x, attackSpeed.y, needs.HappinessLevel);
        currentAttackRange = Mathf.Lerp(attackRange.x, attackRange.y, needs.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(bulletSpeed.x, bulletSpeed.y, needs.HappinessLevel);
    }

    private void SetDefaultValues()
    {
        currentAttackStrength = attackStrength.y;
        currentAttackSpeed = attackSpeed.y;
        currentAttackRange = attackRange.y;
        currentBulletSpeed = bulletSpeed.y;
    }
}
