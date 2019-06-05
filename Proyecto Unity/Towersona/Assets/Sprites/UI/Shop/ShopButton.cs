using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Towersona towersonaToBuild;

    private Button button;
    private TextMeshProUGUI costText;


    private void Awake()
    {            
        button = GetComponent<Button>();     
    }

    private void Start()
    {
        costText = GetComponentInChildren<TextMeshProUGUI>();
        costText.text = towersonaToBuild.stats.buyCost.ToString() + '$';

    }

    private void Update()
    {
        if (!towersonaToBuild) return;

        if(PlayerStats.Instance.money < towersonaToBuild.stats.buyCost && DebuggingOptions.Instance.useMoney)
        {
            button.interactable = false;           
        }
        else
        {
            button.interactable = true;          
        }      
    } 
}
