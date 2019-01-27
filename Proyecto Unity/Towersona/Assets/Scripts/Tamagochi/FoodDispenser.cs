using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    public Camera detailCamera;
    public TowersonaAnimation towersona;

    [SerializeField]
    private float dispenseDelay = 1f;

    [SerializeField]
    private Food foodPrefab = null;

    public void DispenseImmidiately()
    {
        Food newFood = Instantiate(foodPrefab, transform.position, Quaternion.Euler(0f, -90f, 0f));
        newFood.dispenser = this;

        Draggable draggable = newFood.GetComponentInChildren<Draggable>();
        draggable.detailCamera = detailCamera;
        draggable.OnDragStart.AddListener(NotifyFoodDrag);

        towersona.SetIsLookingAtFood(false);
    }

    public void DispenseWithDelay()
    {
        Invoke("DispenseImmidiately", dispenseDelay);
        towersona.SetIsLookingAtFood(false);
    }

    private void NotifyFoodDrag()
    {
        print("Setting to true, fuck you");
        towersona.SetIsLookingAtFood(true);
    }

    private void Awake()
    {
        DispenseImmidiately();
    }
}
