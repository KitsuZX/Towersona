using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailedTowersonaView : MonoBehaviour
{
    public Slider slider;
    public GameObject overHappiness;
    public TowersonaAnimation towersonaAnim;
    public TowersonaNeeds towersonaNeeds;
    public Transform[] shitPositions;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    public TowersonaNeeds SpawnTowersonaNeeds(GameObject towersonaModel)
    {
        GameObject model = Instantiate(towersonaModel);
        model.transform.SetParent(transform, false);
        TowersonaNeeds needs = model.GetComponent<TowersonaNeeds>();

        needs.happinessSlider = slider;
        needs.overHappiness = overHappiness;

        towersonaNeeds = needs;
        towersonaAnim = model.GetComponent<TowersonaAnimation>();
        towersonaAnim.lookAway.camera = cam;

        ShitNeed shitNeed = model.GetComponent<ShitNeed>();
        shitNeed.shitSpawnPositions = shitPositions;

        return needs;
    }
}
