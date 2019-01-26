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
        RaycastHit[] hits = Physics.BoxCastAll(transform.position,
            boxCastHalfSize,
            Vector3.forward,
            Quaternion.identity);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider)
            {
                TowersonaNeeds needs = hits[i].collider.GetComponent<TowersonaNeeds>();
                if (needs)
                {
                    needs.ChangeNeedLevel(TowersonaNeeds.NeedType.Hunger, hungerFulmilmentPerRation);
                    dispenser.DispenseWithDelay();

                    TowersonaAnimation anim = needs.GetComponent<TowersonaAnimation>();
                    anim.SetIsLookingAtFood(false);
                    anim.TriggerEating();

                    Destroy(gameObject);
                }
            }
        }
    }
}
