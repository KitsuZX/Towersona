using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ReturnToPointAfterCountdown), typeof(Draggable))]
public class Food : MonoBehaviour
{
    public float HungerFulmilmentPerRation { get => hungerFulmilmentPerRation; }
    [SerializeField] float hungerFulmilmentPerRation = 1;

    private new Transform transform;
    
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

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Start()
    {
        //Configure things so that we return to the starting position.
        ReturnToPointAfterCountdown returnToPoint = GetComponent<ReturnToPointAfterCountdown>();
        returnToPoint.returnPoint = transform.localPosition;
        returnToPoint.inWorldSpace = false;
    }
}
