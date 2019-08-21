using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersonaHOD : MonoBehaviour
{
    public Slider slider;
    public GameObject overHappiness;
    public Transform[] shitPositions;

    [HideInInspector]
    public Towersona towersona;
    [HideInInspector]
    public TowersonaHODAnimation towersonaAnim;
    [HideInInspector]
    public TowersonaNeeds towersonaNeeds;    

    private Camera cam; 

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    /// <summary>
    /// Spawns the model in the Towersona Detailed Scene. The Detailed scene must have been alredy created.
    /// </summary>   
    public TowersonaNeeds SpawnTowersonaHOD(Towersona towersona, GameObject towersonaHODPrefab)
    {
        /*
         * Código temporal (espero), simplemente va asignando las variables necesarias que necesitan los distintos scripts que hizo Aitor
         * para poder distintos modelos dentro de un DetailedTowersonaView
        */

        GameObject model = Instantiate(towersonaHODPrefab, Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
        model.transform.SetParent(transform, false);              

        TowersonaNeeds needs = model.GetComponent<TowersonaNeeds>();

        this.towersona = towersona;

        needs.happinessSlider = slider;
        needs.overHappiness = overHappiness;

        towersonaNeeds = needs;
        towersonaAnim = model.GetComponent<TowersonaHODAnimation>();

		if(towersonaAnim.lookAway) towersonaAnim.lookAway.m_Camera = cam;

        ShitNeed shitNeed = model.GetComponent<ShitNeed>();
        shitNeed.shitSpawnPositions = shitPositions;

        return needs;
    }    
}
