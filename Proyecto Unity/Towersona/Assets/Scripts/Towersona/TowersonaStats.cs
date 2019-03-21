using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaStats
{
    public Vector2 attackStrength;
    public Vector2 attackSpeed;
    public Vector2 attackRange;
    public Vector2 bulletSpeed;
    public int cost;
    public float hungerDecayPerSecond;
    public float loveDecayPerSecond;
    public float loveIncreasePerDeltaUnit;
    public float happinessImpactPerShit;
    public float shittingInterval;
    public int maxShitCount;

    Towersona towersona;

    public TowersonaStats(Towersona towersona)
    {
        this.towersona = towersona;
        
        List<float> data = CSVReader.ReadTowersonaCSV("Cat Towersona Stats.csv", (int)towersona.towersonaLevel);

        if(data != null)
        {
            AssignData(data);
        }
        else
        {
            Debug.LogError("Ha habido problemas al leer los datos del csv");
        }

    }

    private void AssignData(List<float> data)
    {
        attackStrength.x = data[0];
        attackStrength.y = data[1];

        attackSpeed.x = data[2];
        attackSpeed.y = data[3];

        attackRange.x = data[4];
        attackRange.y = data[5];

        bulletSpeed.x = data[6];
        bulletSpeed.y = data[7];

        cost = (int)data[8];

        hungerDecayPerSecond = data[9];
        loveDecayPerSecond = data[10];
        loveIncreasePerDeltaUnit = data[11];
        happinessImpactPerShit = data[12];
        shittingInterval = data[13];
        maxShitCount = (int)data[14];
    }
}
