using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Cat/Tiger")]
public class TigerStats : TowersonaStats
{
    [Header("Tiger Stats")]
    public Vector2 dañoBala;
    public Vector2 velocidadDeAtaque;
    public Vector2 rango;
    public Vector2 velocidadBala;

    public Vector2 dineroPorSegundo;
    public Vector2 tamañoAreaDañoBala;

    [HideInInspector]
    public float currentMoneyPerSecond;
    [HideInInspector]
    public float currentDamageArea;

    public override void UpdateStats()
    {
        currentAttackStrength = Mathf.Lerp(dañoBala.x, dañoBala.y, needs.HappinessLevel);
        currentAttackSpeed = Mathf.Lerp(velocidadDeAtaque.x, velocidadDeAtaque.y, needs.HappinessLevel);
        currentAttackRange = Mathf.Lerp(rango.x, rango.y, needs.HappinessLevel);
        currentBulletSpeed = Mathf.Lerp(velocidadBala.x, velocidadBala.y, needs.HappinessLevel);

        currentMoneyPerSecond = Mathf.Lerp(dineroPorSegundo.x, dineroPorSegundo.y, needs.HappinessLevel);
        currentDamageArea = Mathf.Lerp(tamañoAreaDañoBala.x, tamañoAreaDañoBala.y, needs.HappinessLevel);
    }

    public override void SetDefaultValues()
    {
        currentAttackStrength = dañoBala.y;
        currentAttackSpeed = velocidadDeAtaque.y;
        currentAttackRange = rango.y;
        currentBulletSpeed = velocidadBala.y;

        currentMoneyPerSecond = dineroPorSegundo.y;
        currentDamageArea = tamañoAreaDañoBala.y;
    }

}
