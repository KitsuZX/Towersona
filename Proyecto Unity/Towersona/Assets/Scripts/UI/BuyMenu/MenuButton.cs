using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class MenuButton : MonoBehaviour
{
	[HideInInspector] public Button button;

	public MenuButtonType type;

	private Image image;
	private Sprite originalSprite;

	private void Awake()
	{
		button = GetComponent<Button>();
		image = transform.GetChild(1).GetComponent<Image>();

		if (image == null)
		{
			Debug.LogError("No se ha encontrado la imagen en el hijo. Recuerda que" +
				"la imagen tiene que estar en segundo lugar");
		}

		originalSprite = image.sprite;
	}

	public void SetSprite(Sprite sprite = null)
	{
		if (sprite == null)
		{
			image.sprite = originalSprite;
		}
		else
		{
			image.sprite = sprite;
		}
	}

	public enum MenuButtonType
	{
		Buy, Upgrade, Sell
	}
}
