using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{   
    [SerializeField] private GameObject UI = null;
    [SerializeField] private GameObject[] UIS = null; 

    [HideInInspector] public bool UIIsActive = false;

	private BuyButton buttonSelected;

	private Towersona towersona;

    public void SetTarget(Towersona towersona)
    {       
        this.towersona = towersona;

        transform.position = towersona.towersonaLOD.transform.position;

        UI.SetActive(true);

        foreach (var UI in UIS)
        {
            if (UI.gameObject.activeSelf) UI.gameObject.SetActive(false);
        }

        CheckInteractivity();
        

        UIIsActive = true;
    }

    public void Hide()
    {
        UI.SetActive(false);

        towersona = null;

        foreach (var UI in UIS)
        {
            if (UI.gameObject.activeSelf) UI.gameObject.SetActive(false);
        }

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
                UIS[0].gameObject.SetActive(true);

                SetButtonInteractivity(UIS[0].gameObject, "Upgrade", 1);
                SetButtonInteractivity(UIS[0].gameObject, "Sell Button", 0, false);

                break;
            case Towersona.TowersonaLevel.LVL2:
                UIS[1].gameObject.SetActive(true);

                SetButtonInteractivity(UIS[1].gameObject, "Ev1", 2);
                SetButtonInteractivity(UIS[1].gameObject, "Ev2", 3);
                SetButtonInteractivity(UIS[1].gameObject, "Sell Button", 1, false);
                break;
            case Towersona.TowersonaLevel.LVL31:
                UIS[2].gameObject.SetActive(true);
                SetButtonInteractivity(UIS[2].gameObject, "Sell Button", 2, false);
                break;
            case Towersona.TowersonaLevel.LVL32:
                UIS[2].gameObject.SetActive(true);
                SetButtonInteractivity(UIS[2].gameObject, "Sell Button", 3, false);
                break;
        }
    }

    private void SetButtonInteractivity(GameObject nodeUI, string buttonName, int costIndex, bool buyingCost = true)
    {
        Button button = nodeUI.transform.GetChild(0).Find(buttonName).GetComponent<Button>();

        if (buyingCost)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonName + ' ' + towersona.statsArray[costIndex].buyCost + '$';

			if (towersona.statsArray[costIndex].buyCost > PlayerStats.Instance.money && DebuggingOptions.Instance.useMoney)
			{
				//Not enough money
				button.interactable = false;
			}
			else
			{
				button.interactable = true;
			}
		}
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Sell \n" + towersona.statsArray[costIndex].sellCost + '$';
        }    
    }
}
