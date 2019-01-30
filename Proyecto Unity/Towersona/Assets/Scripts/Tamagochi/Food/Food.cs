using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    float hungerFulmilmentPerRation = 1;
    [SerializeField]
    Vector3 boxCastHalfSize = Vector3.one;

    [HideInInspector]
    public FoodDispenser dispenser = null;

    public void OnLettingGo()
    {    
        dispenser.towersonaAnim.SetIsLookingAtFood(false);
        GetEaten(dispenser.towersonaNeeds);
        Destroy(gameObject);
    }
    
    private void GetEaten(TowersonaNeeds towersonaNeeds)
    {
        towersonaNeeds.ChangeNeedLevel(TowersonaNeeds.NeedType.Hunger, hungerFulmilmentPerRation);
        dispenser.DispenseWithDelay();

        TowersonaAnimation anim = towersonaNeeds.GetComponent<TowersonaAnimation>();
        anim.SetIsLookingAtFood(false);
        anim.TriggerEating();

        Destroy(gameObject);
    }

}
