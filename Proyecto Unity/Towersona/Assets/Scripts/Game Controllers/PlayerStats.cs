using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Starting Parameters")]
    public int wavesToWin = 10;
    [SerializeField]
    [Tooltip("Starting lifes of the player")]
    private int startingLives = 10;
    [SerializeField]
    [Tooltip("Starting money the player has")]
    private int startingMoney = 100;

    [Header("Current parameters. No need to touch")]
    public int lives;
    public int money;
    public int round;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);

        lives = startingLives;
        money = startingMoney;
    }

    /// <summary>
    /// Takes out a life. If the number of lifes is less than 0, calls Game Over. Also adds Camera Shake.
    /// </summary>
    public void LoseLive(int lives)
    {
        this.lives -= lives;
        if (lives <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            CameraShake.Instance.AddTrauma(0.5f);
        }

        InGameUIController.Instance.UpdateLives();
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        InGameUIController.Instance.UpdateMoney();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        InGameUIController.Instance.UpdateMoney();
    }

    public void AddRound()
    {
        round++;
        InGameUIController.Instance.UpdateRound();
    }

    private void Update()
    {
        if (round == wavesToWin)
        {
            GameManager.Instance.WinGame();
        }
    }
}
