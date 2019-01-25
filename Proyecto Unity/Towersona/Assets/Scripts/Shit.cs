using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : MonoBehaviour
{
    [HideInInspector]
    public ShitNeed origin;

    private void OnMouseUpAsButton()
    {
        Clean();
    }

    public void Clean()
    {
        origin.Remove(this);
        Destroy(gameObject);
    }
}
