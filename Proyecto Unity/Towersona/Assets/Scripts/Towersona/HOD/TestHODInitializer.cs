using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowersonaHODSetup))]
public class TestHODInitializer : MonoBehaviour
{
    [SerializeField] private GameObject towersonaHOD;
    [SerializeField] private TowersonaStats stats;

    private void Start()
    {
        GetComponent<TowersonaHODSetup>().SpawnTowersonaHOD(stats, towersonaHOD);
    }
}
