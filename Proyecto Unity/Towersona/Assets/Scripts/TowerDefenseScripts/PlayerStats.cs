using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int Lives = 30;
    public static int Rounds = 0;    

    public static void LoseLife()
    {
        Lives--;
        if (Lives <= 0)
        {
            TowerDefenseManager.Instance.GameOver();
        }
    }
}
