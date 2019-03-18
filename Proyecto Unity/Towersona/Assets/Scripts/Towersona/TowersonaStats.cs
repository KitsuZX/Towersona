using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaStats
{  
    [Tooltip("Min, Max")]
    public Vector2 attackStrength = new Vector2(0.5f, 10f);
    [Tooltip("Min, Max")]
    public Vector2 attackSpeed = new Vector2(0.25f, 2f);   
    [Tooltip("Min, Max")]
    public Vector2 attackRange = new Vector2(1f, 5);
    [Tooltip("Min, Max")]
    public Vector2 bulletSpeed = new Vector2(2f, 10f);

    Towersona towersona;

    public TowersonaStats(Towersona towersona)
    {
        this.towersona = towersona;

        CSVReader.ReadCSV("Cat Towersona Stats.csv");
    }
}
