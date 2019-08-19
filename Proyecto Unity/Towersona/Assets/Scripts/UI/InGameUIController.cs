using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIController : MonoBehaviour
{
    public static InGameUIController Instance { get; private set; }

    [Header("UI Texts References")]
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private TextMeshProUGUI roundText;
    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void Start()
    {
        if (!Instance) Instance = this;
        else Destroy(this);

        livesText.text = "lives: " + PlayerStats.Instance.lives.ToString();
        roundText.text = "round: " + PlayerStats.Instance.round.ToString() + "/" + LevelManager.Instance.wavesToWin;
        moneyText.text = "money: " + PlayerStats.Instance.money.ToString() + "$";
    }

    public void UpdateLives()
    {
        livesText.text = "lives: " + PlayerStats.Instance.lives.ToString();
    }

    public void UpdateRound()
    {
        roundText.text = "round: " + PlayerStats.Instance.round.ToString() + "/" + LevelManager.Instance.wavesToWin;
    }

    public void UpdateMoney()
    {
        moneyText.text = "money: " + PlayerStats.Instance.money.ToString() + "$";
    }
}
