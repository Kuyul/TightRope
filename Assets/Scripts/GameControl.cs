using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public Touch TouchScript;
    
    public float gaugeTime;
    public Slider slider;
    public Animator anim;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if touching screen, increase slider value
        if (TouchScript.touching == true)
        {
            stop = false;
            timeTracker = timeTracker + Time.deltaTime;
            slider.value = timeTracker / gaugeTime;
        }
        // release touch to trigger perfect if slider is between x and y values
        else
        {
            if (slider.value > 0.5 && slider.value < 0.8 && !stop)
            {
                Perfect();
            }
            stop = true;
            timeTracker = 0;
        }


    }

    // function called on perfect stop.
    public void Perfect()
    {
        Debug.Log("perfect");
    }

    public void IncrementStepNumber()
    {
        // odd number is left trigger, even number is right trigger
        stepNumber++;
    }

    public void MoveAnim()
    {
        if (stepNumber % 2 == 0)
        {
            anim.SetTrigger("rightstep");
        }
        else
        {
            anim.SetTrigger("leftstep");
        }
    }
}
