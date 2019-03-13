using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NodeUI : MonoBehaviour
{
    private Towersona target;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    public GameObject lvl1UI;
    [SerializeField]
    public GameObject lvl2UI;

    [HideInInspector]
    public bool UIIsActive = false;

    public void SetTarget(Towersona towersona)
    {
        target = towersona;

        transform.position = towersona.transform.position;

        UI.SetActive(true);

        if (lvl1UI.gameObject.activeSelf) lvl1UI.gameObject.SetActive(false);
        if (lvl2UI.gameObject.activeSelf) lvl2UI.gameObject.SetActive(false);

        switch (towersona.towersonaLevel)
        {
            case Towersona.TowersonaLevel.LVL1:
                lvl1UI.gameObject.SetActive(true);
                break;
            case Towersona.TowersonaLevel.LVL2:
                lvl2UI.gameObject.SetActive(true);
                break;
        }

        UIIsActive = true;
    }

    public void Hide()
    {
        UI.SetActive(false);

        if(lvl1UI.gameObject.activeSelf) lvl1UI.gameObject.SetActive(false);
        if(lvl2UI.gameObject.activeSelf) lvl2UI.gameObject.SetActive(false);

        UIIsActive = false;
    }   
}
