using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649
public class TowersonaHODSetup : MonoBehaviour
{
    [SerializeField] private Transform towersonaHODParent;

    /// <summary>
    /// Spawns the model in the Towersona Detailed Scene. The Detailed scene must have been alredy created.
    /// </summary>   
    public TowersonaNeeds SpawnTowersonaHOD(Towersona towersona, GameObject towersonaHODPrefab)
    {
        //Instantiate the towersona HOD
        GameObject towersonaHodGO = Instantiate(towersonaHODPrefab, towersonaHODParent, false);
        TowersonaNeeds needs = towersonaHodGO.GetComponent<TowersonaNeeds>();

        needs.SetStats(towersona.stats);
        needs.Reset();

        return needs;
    }    
}
