﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

#pragma warning disable 649
public class TowersonaHODSetup : MonoBehaviour
{
    [SerializeField, Required] private Transform towersonaHODParent;
    [SerializeField, Required] private LoveNeedUI loveNeedUI;

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
        loveNeedUI.SetWatchedLoveNeed(needs.LoveNeed);

        return needs;
    }    
}
