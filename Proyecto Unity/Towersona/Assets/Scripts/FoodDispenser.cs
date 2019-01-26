using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDispenser : MonoBehaviour
{
    [SerializeField]
    private Food foodPrefab;

    public void Dispense()
    {
        Food newFood = Instantiate(foodPrefab, transform.position, Quaternion.identity);
        newFood.dispenser = this;
    }

    private void Awake()
    {
        Dispense();
    }
}
