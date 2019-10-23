using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointerInput
{ 
    public static bool IsTouching
    {
        get
        {
#if UNITY_EDITOR
            return Input.touchCount > 0 || Input.GetMouseButton(0);
#else
            return Input.touchCount > 0;
#endif
        }
    }

    public static bool ReadPosition(out Vector2 position)
    {
        if (Input.touchCount > 0)
        {
            position = Input.GetTouch(0).position;
            return true;
        }
#if UNITY_EDITOR
        else if (Input.GetMouseButton(0))
        {
            position = Input.mousePosition;
            return true;
        }
#endif
        else
        {
            position = Vector2.zero;
            return false;
        }
    }
}
