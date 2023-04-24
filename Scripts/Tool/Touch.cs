using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public static bool SingleTap(int x)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(x))
        {
            var pos = Input.mousePosition;
            return true;
        }
#else
            if (Input.touchCount > 0)  
            {
                var touch = Input.GetTouch(x);  
                if (touch.phase == TouchPhase.Began)  
                {
                    var pos = touch.position;  
                    return true;
                }
            }
#endif
        return false;
    }
}