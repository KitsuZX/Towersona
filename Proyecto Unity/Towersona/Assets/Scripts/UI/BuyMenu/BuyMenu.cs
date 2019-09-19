using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

	GameObject activeUI;

	private void Awake()
	{
        buttons = GetComponentsInChildren<Button>();

		for (int i = 0; i < texts.Length; i++)
		{
			Towersona t = BuildManager.Instance.towersonaPrefabs[i];
			if(t == null)
			{
				Debug.LogError("No se ha encontrado la towersona. ¿Están en el orden correcto en BuildManager y en BuyMenu?");
			}
			else
			{
				texts[i].text = t.stats.buyCost + "$";
			}			
		}
        
        InvokeRepeating("CheckMoney", 0f, 0.5f);
	}

	public void SetPlace(BuildingPlace _place)
	{
		place = _place;		
		Vector3 pos = _place.transform.position;
		pos.y += 1f;
		pos.z += 1f;

		transform.position = pos;

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
				ShowInfo(buyButton.towersona, -1);
				break;
			case MenuButton.MenuButtonType.Upgrade:
				button.button.onClick.AddListener(OnUpgradeConfirmed);
				UpgradeButton upgradeButton = (UpgradeButton)button;
				BuildManager.Instance.ShowMinMaxRange(upgradeButton.upgradeIndex);
				ShowInfo(towersona, upgradeButton.upgradeIndex);
				break;

			case MenuButton.MenuButtonType.Sell:
				button.button.onClick.AddListener(OnSellConfirmed);	
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
            Towersona t = BuildManager.Instance.towersonaPrefabs[i];
            if(t.stats.buyCost > PlayerStats.Instance.money)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].interactable = true;
            }
        }
    }
}
