using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public Camera detailCamera;
    public TowersonaAnimation towersonaAnim;
    public TowersonaNeeds towersonaNeeds;

    [HideInInspector]
    public Towersona towersona;

    [SerializeField]
    private float dispenseDelay = 1f;

    [SerializeField]
    private Food foodPrefab = null;

    public void DispenseImmidiately()
    {
        Food newFood = Instantiate(foodPrefab, transform.position, Quaternion.identity);
        newFood.dispenser = this;
        newFood.transform.SetParent(transform);

        Draggable draggable = newFood.GetComponentInChildren<Draggable>();
        draggable.detailCamera = detailCamera;
        draggable.OnDragStart.AddListener(NotifyFoodDrag);

        towersonaAnim.SetIsLookingAtFood(false);
        towersonaAnim.SetLookAtTransform(draggable.transform);
    }

    public void DispenseWithDelay()
    {
        Invoke("DispenseImmidiately", dispenseDelay);
        towersonaAnim.SetIsLookingAtFood(false);
    }

    private void NotifyFoodDrag()
    {
        towersonaAnim.SetIsLookingAtFood(true);    
    }

    private void Awake()
    {
        DispenseImmidiately();
    }
}
