using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITesting : MonoBehaviour
{
    [SerializeField]
    private VictoryPrompt victoryPrompt;

    private void Awake()
    {
        victoryPrompt.ShowVictoryPrompt(3);
    }
}
