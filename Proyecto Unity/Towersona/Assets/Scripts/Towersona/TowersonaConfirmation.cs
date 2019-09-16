using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersonaConfirmation : MonoBehaviour
{
    [SerializeField] private Confirmation[] confirmations;
    public Dictionary<GameObject, GameObject> models = new Dictionary<GameObject, GameObject>();

    private GameObject activeModel;

    private void Awake()
    {
        for (int i = 0; i < confirmations.Length; i++)
        {
            models.Add(confirmations[i].towersona, confirmations[i].model);
        }
    }

    public void ActivateModel(Vector3 position, GameObject towersona)
    {
        transform.position = position;

        if(activeModel != null)
        {
            DesactivateModel();
        }

        activeModel = models[towersona];
        activeModel.transform.eulerAngles = new Vector3(-17, 180, 0);
        activeModel.SetActive(true);        
    }

    public void DesactivateModel()
    {
        activeModel.SetActive(false);
    }  
}

[System.Serializable]
public struct Confirmation
{
    public GameObject towersona;
    public GameObject model;
}

