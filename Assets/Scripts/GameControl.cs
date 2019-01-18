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
    public float gaugeTime;
    public Slider slider;

    private float timeTracker = 0;
    private bool stop;
    private int stepNumber=0;

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
        if (!Player.IsAnimationPlaying())
        {
            // if touching screen, increase slider value
            if (TouchScript.touching == true)
            {
                stop = false;
                timeTracker = timeTracker + Time.deltaTime;
                slider.value = timeTracker / gaugeTime;
            }
            /* Moved this section out of update because this section gets called at the start of the game when it should really be called only when touch is lifted
            else
            {
                if (slider.value > 0.5 && slider.value < 0.8 && !stop)
                {
                    Perfect();
                }
                stop = true;
                timeTracker = 0;
            }*/
        }
    }

    //This method is called from the Touch Script
    //release touch to trigger perfect if slider is between x and y values
    public void TouchLifted()
    {
        if (slider.value > 0.5 && slider.value < 0.8 && !stop)
        {
            Perfect();
        }
        else
        {
            NotPerfect();
        }
        stop = true;
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
