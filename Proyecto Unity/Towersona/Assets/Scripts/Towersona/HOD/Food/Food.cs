using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    float hungerFulmilmentPerRation = 1; 

    [HideInInspector]
    public FoodDispenser dispenser = null;
    
    //TODO: Hacer que solo se coma la comida cuando se le arrastra la cabeza, si no no. 
    public void OnLettingGo()
    {  
        /*
        //TODO: Search for a collider in Feeding layer. If hit a collider, look for a FoodNeed component.
        dispenser.towersonaAnim.SetIsLookingAtFood(false);
        dispenser.towersonaNeeds.FoodNeed.Feed(hungerFulmilmentPerRation);
        dispenser.DispenseWithDelay();

        //This should REALLY not be done here. Use an event in FoodNeed instead.
        TowersonaHODAnimation anim = dispenser.towersonaNeeds.GetComponent<TowersonaHODAnimation>();
        anim.Eat();

        //No estaría de más reciclar este objeto en vez de destruirlo y crear otro nuevo. Así evitamos generar basura.
        Destroy(gameObject);
        */
    }
}
