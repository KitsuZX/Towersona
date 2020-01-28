using UnityEngine;

#pragma warning disable 649
public class UITesting : MonoBehaviour
{
    [SerializeField]
    private VictoryPrompt victoryPrompt;

    private void Awake()
    {
        victoryPrompt.ShowVictoryPrompt(3);
    }
}
