using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private Image fillImg;
    private float timeAmt;

    private TowersController towersController;
    private float countdownTillNewTowersona;
    private Button button;


    private void Awake()
    {
        towersController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TowersController>();
        fillImg = transform.Find("Countdown").GetComponent<Image>();
        button = GetComponent<Button>();

        timeAmt = towersController.timeBetweenTowersonas;
        countdownTillNewTowersona = timeAmt;

        fillImg.fillAmount = 0;
    }

    private void Update()
    {
        //Towersona building    
        if (!towersController.towerAvaible)
        {
            countdownTillNewTowersona -= Time.deltaTime;
            fillImg.fillAmount = countdownTillNewTowersona / timeAmt;

            button.interactable = false;

            if (countdownTillNewTowersona <= 0f)
            {
                towersController.towerAvaible = true;
                countdownTillNewTowersona = timeAmt;
                button.interactable = true;
            }         
        }
      
    }
}
