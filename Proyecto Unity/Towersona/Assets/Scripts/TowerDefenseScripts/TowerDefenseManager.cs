using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseManager : MonoBehaviour
{
    private WorldGenerator worldGenerator;
    private WavesController wavesController;

    void Awake()
    {        
        worldGenerator = GetComponent<WorldGenerator>();
        wavesController = GetComponent<WavesController>();
    }

    private void Start() {
        worldGenerator.GenerateWorld();
        worldGenerator.GeneratePath();
    }


    void Update()
    {
        
    }    
}
