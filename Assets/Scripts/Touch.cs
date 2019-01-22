using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public PlayerControl player;
    public bool touch;

    //Initialise Dragging
    public void OnPointerDown(PointerEventData eventData)
    {
        touch = true;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touch = false;
        GameControl.instance.Touched();
    }
}
