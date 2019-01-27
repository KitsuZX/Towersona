using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int Lives = 30;
    public static int Rounds = 0;
    public static bool TowerAvaible = true;
    public static bool MaxReached = false;

    public static void LoseLife()
    {
        Lives--;
        if (Lives <= 0)
        {
            TowerDefenseManager.Instance.GameOver();
            CameraShake.Instance.AddTrauma(0.8f);

        }
        else
        {
            CameraShake.Instance.AddTrauma(0.5f);
        }
    }
}
