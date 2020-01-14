using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnKey : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent OnKeyDown;

    private void Update()
    {
        if (Input.GetKeyDown(key)) OnKeyDown.Invoke();
    }
}
