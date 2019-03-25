using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlace : MonoBehaviour
{

    private bool hasTower = false;

    private void OnMouseUpAsButton()
    {
        if (!hasTower)
        {
            BuildManager.Instance.SpawnTowersona(this);
        }
    }
}
