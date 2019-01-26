using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isPath = false;
    public Vector2 position;

    [SerializeField]
    private Material pathMaterial;

    public void MakePath()
    {
        isPath = true;
        GetComponent<MeshRenderer>().material = pathMaterial;
    }
}
