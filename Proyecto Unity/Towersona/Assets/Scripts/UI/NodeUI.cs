using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NodeUI : MonoBehaviour
{
    private Towersona target;
    [SerializeField]
    private GameObject UI;

    [HideInInspector]
    public bool UIIsActive = false;

    public void SetTarget(Towersona towersona)
    {
        target = towersona;

        transform.position = towersona.transform.position;

        UI.SetActive(true);

        UIIsActive = true;
    }

    public void Hide()
    {
        UI.SetActive(false);
        UIIsActive = false;
    }
}
