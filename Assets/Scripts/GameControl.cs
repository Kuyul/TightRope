using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    //Declare instance
    public static GameControl instance;

    //Declare public variables
    public Touch TouchScript;
    public PlayerControl Player;
    public Slider slider;

    private float timeTracker = 0;
    private float timeLimit = 0;
 
    private bool sliderFill = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if animation is currently being played
        // if touching screen, increase slider value

        if (TouchScript.touch)
        {
            float delta;
            if (sliderFill)
            {
                delta = Time.deltaTime;
            }
            else
            {
                delta = -Time.deltaTime;
            }

            timeTracker = timeTracker + delta;
            timeLimit = Mathf.Pow(timeTracker, 3f);
            slider.value = timeLimit;

            if (slider.value >= slider.maxValue)
            {
                sliderFill = false;
            }
            else if (slider.value <= slider.minValue)
            {
                sliderFill = true;
            }
        }
        
    }

    //This method is called from the Touch Script
    //release touch to trigger perfect if slider is between x and y values
    public void Touched()
    {
        if (slider.value > 0.5 && slider.value < 0.8)
        {
            Perfect();
        }
        else
        {
            NotPerfect();
        }
        sliderFill = true;
        timeTracker = 0;
    }

    // function called on perfect stop.
    public void Perfect()
    {
        Player.Perfect();
        Debug.Log("perfect");
    }

    public void NotPerfect()
    {
        Player.NotPerfect();
        Debug.Log("not perfect");
    }

}
