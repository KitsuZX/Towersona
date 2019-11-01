using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicButton : MonoBehaviour
{
    public void Press()
    {
        foreach (TowersonaNeeds needs in GlobalTowersonaNeedProvider.GetAll())
        {
            needs.ResetNeeds();
        }
    }
}
