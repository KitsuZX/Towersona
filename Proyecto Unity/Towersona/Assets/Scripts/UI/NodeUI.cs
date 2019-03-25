using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private Towersona towersona;

    public void SetTarget(Towersona towersona)
    {
        target = towersona;
        this.towersona = towersona;

        transform.position = towersona.towersonaLOD.transform.position;

        UI.SetActive(true);

        if (lvl1UI.gameObject.activeSelf) lvl1UI.gameObject.SetActive(false);
        if (lvl2UI.gameObject.activeSelf) lvl2UI.gameObject.SetActive(false);

        CheckInteractivity();
        

        UIIsActive = true;
    }

    public void Hide()
    {
        UI.SetActive(false);

        towersona = null;

        if(lvl1UI.gameObject.activeSelf) lvl1UI.gameObject.SetActive(false);
        if(lvl2UI.gameObject.activeSelf) lvl2UI.gameObject.SetActive(false);

        UIIsActive = false;
    }

    private void Update()
    {
        if (towersona == null) return;
        CheckInteractivity();
    }

    private void CheckInteractivity()
    {
        switch (towersona.towersonaLevel)
        {
            case Towersona.TowersonaLevel.LVL1:
                lvl1UI.gameObject.SetActive(true);

                SetButtonInteractivity(lvl1UI.gameObject, "Upgrade", 1);

                break;
            case Towersona.TowersonaLevel.LVL2:
                lvl2UI.gameObject.SetActive(true);

                SetButtonInteractivity(lvl2UI.gameObject, "Ev1", 2);
                SetButtonInteractivity(lvl2UI.gameObject, "Ev2", 3);
                break;
        }
    }

    private void SetButtonInteractivity(GameObject nodeUI, string buttonName, int costIndex)
    {
        Button button = nodeUI.transform.GetChild(0).Find(buttonName).GetComponent<Button>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = buttonName + ' ' + towersona.costs[costIndex] + '$';

        if (towersona.costs[2] > PlayerStats.Instance.money && DebuggingOptions.Instance.useMoney)
        {
            //Not enough money
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
