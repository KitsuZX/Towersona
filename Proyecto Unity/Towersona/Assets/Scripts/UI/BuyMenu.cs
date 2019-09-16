using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenu : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI[] texts = null;
    [SerializeField] private Sprite confirmationSprite = null;

    private BuildingPlace place;
    private Button[] buttons;
    private BuyButton buttonSelected;   

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
				texts[i].text = t.menuName + " " + t.stats.buyCost + "$";
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
		
		gameObject.SetActive(true);

        if (DebuggingOptions.Instance.useMoney)
        {
            CheckMoney();
        }
	}

	public void Hide()
	{
        DeselectButton();
		gameObject.SetActive(false);
	}

    public void ConfirmPurchase(BuyButton buyButton)
    {     
        SelectButton(buyButton);
    }

    private void BuyTowersona(Towersona towersona)
    {
        BuildManager.Instance.SpawnTowersona(place, towersona);
        Hide();
    }

    private void SelectButton(BuyButton button)
    {
        if(buttonSelected != null)
        {
            DeselectButton();
        }

        buttonSelected = button;
        button.SetSprite(confirmationSprite);  
        button.button.onClick.AddListener(OnPurchaseConfirmed);
    }

    private void DeselectButton()
    {
        buttonSelected.SetSprite();
        buttonSelected.button.onClick.RemoveListener(OnPurchaseConfirmed);
        buttonSelected = null;        
    }

    private void OnPurchaseConfirmed()
    {
        BuyTowersona(buttonSelected.towersona);
    }

    private void CheckMoney()
    {
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
