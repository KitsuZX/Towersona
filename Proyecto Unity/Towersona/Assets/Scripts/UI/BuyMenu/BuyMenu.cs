using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuyMenu : MonoBehaviour
{
	public bool IsActive
	{
		get
		{
			return activeUI != null;
		}
	}

	[SerializeField] private TextMeshProUGUI[] texts = null;
    [SerializeField] private Sprite confirmationSprite = null;

	[SerializeField] private GameObject lvl0 = null;
	[SerializeField] private GameObject lvl1 = null;
	[SerializeField] private GameObject lvl2 = null;
	[SerializeField] private GameObject lvl3 = null;
	[SerializeField] private PurchaseInfo purchaseInfo = null;

	private BuildingPlace place;
    private Button[] buttons;
    private MenuButton buttonSelected;
	private Towersona towersona;

	private Towersona previousTowersonaBuilt = null;
	private int inflationStack = 0;

	GameObject activeUI;

	private Coroutine showInfoCoroutine;

	private float secondsToShowInfo = 0.5f;
	private float timeLeft = 1f;

	private void Awake()
	{
        buttons = GetComponentsInChildren<Button>(true);
		UpdateMoneyTexts();    		
	}

	public void SetPlace(BuildingPlace _place)
	{
		place = _place;		
		/*Vector3 pos = _place.transform.position;
		pos.y += 1f;
		pos.z += 1f;

		transform.position = pos;*/

		if (!_place.hasTower)
		{
			lvl0.SetActive(true);
			activeUI = lvl0;
		}
		else
		{
			Towersona t = place.towersona;
			towersona = t;
			switch (t.towersonaLevel)
			{
				case Towersona.TowersonaLevel.LVL1:
					lvl1.SetActive(true);
					activeUI = lvl1;
					break;
				case Towersona.TowersonaLevel.LVL2:
					lvl2.SetActive(true);
					activeUI = lvl2;
					break;
				case Towersona.TowersonaLevel.LVL31:
					lvl3.SetActive(true);
					activeUI = lvl3;
					break;
				case Towersona.TowersonaLevel.LVL32:
					lvl3.SetActive(true);
					activeUI = lvl3;
					break;
			}			
		}

		UpdateMoneyTexts();
		CheckMoney();
	}

	public void Hide()
	{
        DeselectButton();
		HideInfo();
		activeUI.SetActive(false);
		activeUI = null;
	}

    public void ConfirmButtonPressed(MenuButton button)
    {     
        SelectButton(button);
    }

    private void SelectButton(MenuButton button)
    {
        if(buttonSelected != null)
        {
            DeselectButton();
        }

        buttonSelected = button;
        button.SetSprite(confirmationSprite);

		switch (button.type)
		{
			case MenuButton.MenuButtonType.Buy:
				button.button.onClick.AddListener(OnPurchaseConfirmed);
				BuyButton buyButton = (BuyButton)button;
				BuildManager.Instance.SetTowersonaConfirmation(place, buyButton.towersona);				
				break;
			case MenuButton.MenuButtonType.Upgrade:
				button.button.onClick.AddListener(OnUpgradeConfirmed);
				UpgradeButton upgradeButton = (UpgradeButton)button;
				BuildManager.Instance.ShowMinMaxRange(upgradeButton.upgradeIndex);		
				break;

			case MenuButton.MenuButtonType.Sell:
				button.button.onClick.AddListener(OnSellConfirmed);	
				break;
		}     
    }

	private void ShowInfo(MenuButton button)
	{
		if (buttonSelected != null)
		{
			DeselectButton();
		}

		buttonSelected = button;	

		switch (button.type)
		{
			case MenuButton.MenuButtonType.Buy:				
				BuyButton buyButton = (BuyButton)button;		
				ShowInfo(buyButton.towersona, -1);
				break;
			case MenuButton.MenuButtonType.Upgrade:			
				UpgradeButton upgradeButton = (UpgradeButton)button;
				ShowInfo(towersona, upgradeButton.upgradeIndex);
				break;
			case MenuButton.MenuButtonType.Sell:			
				break;
		}
	}

	private void ShowInfo(Towersona towersona, int updgradeIndex)
	{
		purchaseInfo.gameObject.SetActive(true);
		purchaseInfo.SetInfo(towersona, updgradeIndex);
	}

	private void HideInfo()
	{
		purchaseInfo.gameObject.SetActive(false);
	}

    private void DeselectButton()
    {
		if (buttonSelected == null) return;	
        buttonSelected.SetSprite();
		switch (buttonSelected.type)
		{
			case MenuButton.MenuButtonType.Buy:
				buttonSelected.button.onClick.RemoveListener(OnPurchaseConfirmed);
				BuildManager.Instance.DestroyTowersonaConfirmation();				
				break;
			case MenuButton.MenuButtonType.Upgrade:
				buttonSelected.button.onClick.RemoveListener(OnUpgradeConfirmed);
				break;

			case MenuButton.MenuButtonType.Sell:
				buttonSelected.button.onClick.RemoveListener(OnSellConfirmed);
				break;
		}
		
        buttonSelected = null;        
    }

    private void OnPurchaseConfirmed()
    {
		BuyButton buyButton = (BuyButton)buttonSelected;
		BuildManager.Instance.SpawnTowersona(place, buyButton.towersona);

		int baseCost = buyButton.towersona.stats.buyCost;
		int cost = baseCost;
		if (buyButton.towersona == previousTowersonaBuilt)
		{
			cost += Mathf.FloorToInt(baseCost * inflationStack / 3);
			inflationStack++;
		}
		else
		{
			previousTowersonaBuilt = buyButton.towersona;
			inflationStack = 1;
		}

		PlayerStats.Instance.SpendMoney(cost);
		Hide();		
	}

	private void OnUpgradeConfirmed() {
		UpgradeButton button = (UpgradeButton)buttonSelected;
		BuildManager.Instance.UpgradeTowersona(button.upgradeIndex);
	}

	private void OnSellConfirmed()
	{
		BuildManager.Instance.SellTowersona();
	}

    private void CheckMoney()
    {
        if (!DebuggingOptions.Instance.useMoney) return;

        for (int i = 0; i < buttons.Length; i++)
        {
			BuyButton buyButton = buttons[i].GetComponent<BuyButton>();

			if (buyButton == null) return;

			Towersona t = buyButton.towersona;

			int baseCost = t.stats.buyCost;
			int cost = baseCost;

			if(t == previousTowersonaBuilt)
			{
				cost += baseCost * inflationStack / 3;
			}

            if(cost > PlayerStats.Instance.money)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].interactable = true;
            }
        }
    }

	private void UpdateMoneyTexts()
	{
		for (int i = 0; i < texts.Length; i++)
		{
			Towersona t = BuildManager.Instance.towersonaPrefabs[i];
			if (t == null)
			{
				Debug.LogError("No se ha encontrado la towersona. ¿Están en el orden correcto en BuildManager y en BuyMenu?");
			}
			else
			{
				float baseCost = t.stats.buyCost;
				float cost = baseCost;
				if (t == previousTowersonaBuilt)
				{
					cost += Mathf.Round(baseCost * inflationStack / 3);
				}
				texts[i].text = cost + "$";
			}
		}
	}

	public void StartCountdown(MenuButton button)
	{
		timeLeft = secondsToShowInfo;
		showInfoCoroutine = StartCoroutine(Countdown(button));
	}

	public void StopCountdown()
	{
		StopCoroutine(showInfoCoroutine);
		HideInfo();
	}

	private IEnumerator Countdown(MenuButton button)
	{
		while (true)
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0)
			{
				ShowInfo(button);
			}

			yield return new WaitForEndOfFrame();
		}
	}

}
