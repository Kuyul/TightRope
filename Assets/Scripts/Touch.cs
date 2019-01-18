using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public PlayerControl player;
    public bool touching;

    //Initialise Dragging
    public void OnPointerDown(PointerEventData eventData)
    {
        touching = true;  
    }

    //End Dragging
    public void OnPointerUp(PointerEventData eventData)
    {
        GameControl.instance.TouchLifted();
        touching = false;
    }
}
