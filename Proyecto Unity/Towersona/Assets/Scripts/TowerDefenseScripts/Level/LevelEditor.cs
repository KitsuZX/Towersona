using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;

public class LevelEditor : MonoBehaviour
{
    MeshRenderer meshRenderer;

    [Header("Fondo")]
    public Texture2D background;
    [Header("Oleadas")]
    public int numCaminos;
    public int numOleadas;

    private void OnValidate()
    {      
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial.mainTexture = background;
    }   
}


