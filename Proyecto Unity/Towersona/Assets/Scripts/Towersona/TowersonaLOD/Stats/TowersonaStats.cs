using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowersonaStats : ScriptableObject
{
    [Header("Towersonsa stats")]
    [Tooltip("Cuanta felicidad gana la towersona por caricia")]
    public float felicidadPorCaricia;
    [Tooltip("Hambre que pierde por segundo la towersona")]
    public float hambrePorSegundo;
    [Tooltip("Amor que pierde por segundo la towersona")]
    public float perdidaFelicidadPorSegundo;
    [Tooltip("Cuanto cae el amor por cada mierda que hay sin recoger")]
    public float enfadoPorMierda;
    public float tiempoEntreMierdas;
    public int maxMierdas;

    [HideInInspector]
    public float currentAttackStrength;
    [HideInInspector]
    public float currentAttackSpeed;
    [HideInInspector]
    public float currentAttackRange;
    [HideInInspector]
    public float currentBulletSpeed;

    [HideInInspector]
    public TowersonaNeeds needs;  

    public abstract void UpdateStats();
    public abstract void SetDefaultValues();   
}
