using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

#pragma warning disable 649
public class TowersonaHODSetup : MonoBehaviour
{
    [SerializeField] private Transform towersonaHODParent;
    [SerializeField] private new Camera camera;

    /// <summary>
    /// Spawns the model in the Towersona Detailed Scene. The Detailed scene must have been alredy created.
    /// </summary>   
    public TowersonaNeeds SpawnTowersonaHOD(Towersona towersona, GameObject towersonaHODPrefab)
    {
        //Instantiate the towersona HOD
        GameObject towersonaHodGO = Instantiate(towersonaHODPrefab, towersonaHODParent, false);
        TowersonaNeeds needs = towersonaHodGO.GetComponent<TowersonaNeeds>();
        
        //Set the stats
        needs.SetStats(towersona.stats);
        needs.ResetNeeds();

        //Hook up the UI
        GetComponentInChildren<LoveNeedUI>().SetWatchedLoveNeed(needs.LoveNeed);
        GetComponentInChildren<FoodNeedUI>().SetWatchedFoodNeed(needs.FoodNeed);

        //Give the camera to any Draggables (ie food)
        Draggable[] draggables = GetComponentsInChildren<Draggable>();
        for (int i = 0; i < draggables.Length; i++)
        {
            draggables[i].CasterCamera = camera;
        }


        return needs;
    }    
}
