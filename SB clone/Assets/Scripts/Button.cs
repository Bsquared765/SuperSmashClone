using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent buttonClick;
    // Start is called before the first frame update
    
    void awake()
    {
        if (buttonClick == null) { buttonClick = new UnityEvent(); }
    }
    void OnMouseDown()
    {
        Debug.Log("Click");
        buttonClick.Invoke();
    }
}
