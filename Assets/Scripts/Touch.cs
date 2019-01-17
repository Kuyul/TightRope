using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private bool touching;

    public void OnPointerDown(PointerEventData eventData)
    {
        touching = true;  
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touching = false;
    }
}
