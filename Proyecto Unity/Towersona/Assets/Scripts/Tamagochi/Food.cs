using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    float hungerFulmilmentPerRation;
    [SerializeField]
    Vector3 boxCastHalfSize;

    [HideInInspector]
    public FoodDispenser dispenser;


    public void OnLettingGo()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position,
            boxCastHalfSize,
            Vector3.forward,
            Quaternion.identity);

        bool hitTowersona = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider)
            {
                TowersonaNeeds needs = hits[i].collider.GetComponent<TowersonaNeeds>();
                if (needs)
                {
                    needs.ChangeNeedLevel(TowersonaNeeds.NeedType.Hunger, hungerFulmilmentPerRation);
                    dispenser.DispenseWithDelay();
                    Destroy(gameObject);
                }
            }
        }
    }
}
