using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Cat/Cat")]
public class CatStats : TowersonaStats
{
    [Header("Cat Stats")]
    public Vector2 dañoBala;
    public Vector2 velocidadDeAtaque;
    public Vector2 rango;
    public Vector2 velocidadBala;

    public Vector2 dineroPorSegundo;
    
    [HideInInspector]
    public float currentMoneyPerSecond;

    public override void UpdateStats()
    {       
        currentAttackStrength = Mathf.Lerp(dañoBala.x, dañoBala.y, needs.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(velocidadDeAtaque.x, velocidadDeAtaque.y, needs.HappinessLevel);
        currentAttackRange = Mathf.Lerp(rango.x, rango.y, needs.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(velocidadBala.x, velocidadBala.y, needs.HappinessLevel);

        currentMoneyPerSecond = Mathf.Lerp(dineroPorSegundo.x, dineroPorSegundo.y, needs.HappinessLevel);
    }

    public override void SetDefaultValues()
    {
        currentAttackStrength = dañoBala.y;
        currentAttackSpeed = velocidadDeAtaque.y;
        currentAttackRange = rango.y;
        currentBulletSpeed = velocidadBala.y;

        currentMoneyPerSecond = dineroPorSegundo.y;
    }
}
