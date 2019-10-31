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
    public TowersonaNeeds SpawnTowersonaHOD(TowersonaStats stats, GameObject towersonaHODPrefab)
    {
        //Instantiate the towersona HOD
        GameObject towersonaHodGO = Instantiate(towersonaHODPrefab, towersonaHODParent, false);
        TowersonaNeeds needs = towersonaHodGO.GetComponent<TowersonaNeeds>();
        
        //Set the stats
        needs.SetStats(stats);
        needs.ResetNeeds();

        //Hook up the UI
        GetComponentInChildren<LoveNeedUI>().SetWatchedLoveNeed(needs.LoveNeed);
        GetComponentInChildren<FoodNeedUI>().SetWatchedFoodNeed(needs.FoodNeed);

        return needs;
    }

    /* Hay un bug en PhysicsRaycaster, un componente de Unity.
     * Si se le pone un número de máximo de hits (gracias a lo cual no genera basura) da NullReferenceExceptions.
     * La solución está aquí https://forum.unity.com/threads/physicsraycaster-system-nullreferenceexception-bug.537885/.
     * El problema es que creo que no tenemos forma de tocar PhysicsRaycaster.cs
     * Mientras eso sea así, habrá que joderse, poner el máximo a 0 y dejar que genere basura cada fotograma.
     * */
}
