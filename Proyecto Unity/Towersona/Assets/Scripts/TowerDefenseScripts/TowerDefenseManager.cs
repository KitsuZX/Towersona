using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseManager : MonoBehaviour
{
    public static TowerDefenseManager Instance { get; private set; }

    private WorldGenerator worldGenerator;
    private WavesController wavesController;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        worldGenerator = GetComponent<WorldGenerator>();
        wavesController = GetComponent<WavesController>();
    }

    private void Start() {
        worldGenerator.GenerateWorld();
        worldGenerator.GeneratePath();
    }

    public void GameOver()
    {
        Debug.Log("PERDISTE! LAS TOWERSONAS HAN MUERTO!");
    }

    public void WinGame()
    {
        Debug.Log("GANASTE GORDO PENDEJO");
    }      
}
