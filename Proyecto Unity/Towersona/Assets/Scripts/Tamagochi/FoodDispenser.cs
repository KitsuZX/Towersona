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
        Food newFood = Instantiate(foodPrefab, transform.position, Quaternion.identity);
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
        towersona.SetIsLookingAtFood(true);
        towersona.GetComponent<LookAtFood>().enabled = true;
    }

    private void Awake()
    {
        DispenseImmidiately();
    }
}
