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

    private float timeTracker = 0;
    private bool stop;

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
        else
        {
            if (slider.value > 0.5 && slider.value < 0.8 && !stop)
            {
                Perfect();
            }

            timeTracker = 0;
        }


    }

    // function called on perfect stop.
    public void Perfect()
    {
        stop = true;
        Debug.Log("perfect");
    }
}
