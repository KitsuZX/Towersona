using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    float hungerFulmilmentPerRation = 1; 

    [HideInInspector]
    public FoodDispenser dispenser = null;
    
    //TODO: Hacer que solo se coma la comida cuando se le arrastra la cabeza, si no no

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

        TowersonaHODAnimation anim = towersonaNeeds.GetComponent<TowersonaHODAnimation>();               
        anim.Eat();    

        Destroy(gameObject);
    }

}
