using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Cat/Tiger")]
public class TigerStats : CatStats
{    
    public Vector2 damageArea;

    [HideInInspector]
    public float currentMoneyPerSecond;
    [HideInInspector]
    public float currentDamageArea;

    public override void UpdateStats()
    {
		base.UpdateStats();
        currentDamageArea = Mathf.Lerp(damageArea.x, damageArea.y, needs.HappinessLevel);
    }

    public override void SetDefaultValues()
    {
		base.SetDefaultValues();		
        currentDamageArea = damageArea.y;
    }

}
