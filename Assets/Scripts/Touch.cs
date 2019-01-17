﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public bool touching;

    public void OnPointerDown(PointerEventData eventData)
    {
        touching = true;  
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        touching = false;
        GameControl.instance.IncrementStepNumber();
        GameControl.instance.MoveAnim();
    }

    private void Update()
    {
        
    }
}