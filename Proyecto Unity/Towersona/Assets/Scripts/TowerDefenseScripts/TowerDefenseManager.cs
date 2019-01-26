using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefenseManager : MonoBehaviour
{
    private WorldGenerator worldGenerator;

    void Awake()
    {
        
        worldGenerator = GetComponent<WorldGenerator>();
    }

    private void Start() {
        worldGenerator.GenerateWorld();
        StartCoroutine(worldGenerator.GeneratePath());
    }


    void Update()
    {
        
    }    
}
