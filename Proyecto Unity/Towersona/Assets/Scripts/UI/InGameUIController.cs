using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#pragma warning disable 649
public class InGameUIController : MonoBehaviour
{
    public static InGameUIController Instance { get; private set; }

    [Header("UI Texts References")]
    [SerializeField] private TextMeshProUGUI livesText = null;
    [SerializeField] private TextMeshProUGUI roundText = null;
    [SerializeField] private TextMeshProUGUI moneyText = null;


	[Header("Victory and Defeat References")]
	[SerializeField]
	private VictoryPrompt victoryPrompt;
	[SerializeField]
	private GameObject defeatPrompt;

	private void Start()
    {
        if (!Instance) Instance = this;
        else Destroy(this);

        livesText.text = PlayerStats.Instance.lives.ToString();
        roundText.text = "Round: \n" + PlayerStats.Instance.round.ToString() + "/" + LevelManager.Instance.wavesToWin;
        moneyText.text = PlayerStats.Instance.money.ToString();
    }

    public void UpdateLives()
    {
        livesText.text = PlayerStats.Instance.lives.ToString();
    }

    public void UpdateRound()
    {
        roundText.text = "Round: \n" + PlayerStats.Instance.round.ToString() + "/" + LevelManager.Instance.wavesToWin;
    }

    public void UpdateMoney()
    {
        moneyText.text = PlayerStats.Instance.money.ToString();
    }

	public IEnumerator ShowVictoryPrompt(int score)
	{
		yield return new WaitForSeconds(2f);
		victoryPrompt.ShowVictoryPrompt(score);
		SaveSystem.SaveLevel(LevelManager.Instance.LevelIndex, score);

		//Time.timeScale = 0;
	}

	public IEnumerator ShowDefeatPrompt()
	{
		yield return new WaitForSeconds(2f);
		//victoryPrompt.ShowVictoryPrompt(playerStats.Score);
		//SaveSystem.SaveLevel(LevelManager.Instance.LevelIndex, playerStats.Score);

		//Time.timeScale = 0;
	}
}
