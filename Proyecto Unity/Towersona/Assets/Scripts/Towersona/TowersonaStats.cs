using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaStats : MonoBehaviour
{
    [HideInInspector]
    public TowersonaNeeds towersonaNeeds;
    [HideInInspector]
    public Transform firePoint;

    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
    }

}
