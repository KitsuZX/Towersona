using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public int Index => index;
    public int Score => score;
    public bool Available => available;

    private int index;
    private int score;
    private bool available;  

    public bool Completed
    {
        get
        {
            return score != 0;
        }
    }

    public LevelData(int index, int score, bool available)
    {
        this.index = index;
        this.score = score;
        this.available = available;
    }

    public void SetScore(int score)
    {
        this.score = score;
        SavedData.SaveLevel(this);
    }
}
