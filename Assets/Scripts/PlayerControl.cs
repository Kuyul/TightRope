using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Declare public variables
    public Touch TouchScript;
    public LevelControl LevelController;
    public Animator Anim;
    public List<string> movingAnimStates;
    public float speed = 5.0f;
    public float movePointsSpeed = 10.0f;

    //Declare private variables
    private bool MoveToNextPoint = false;
    private Vector3 NextPoint;
    private int StepNumber = 0;
    private int ConsecutivePerfects = 0;
    private List<string> AnimTriggers;
    private Rigidbody rb;

    private void Start()
    {
        AnimTriggers = new List<string>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var isMoving = IsMovingState();
        if (isMoving)
        {
            rb.velocity = transform.forward * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if (MoveToNextPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPoint, movePointsSpeed * Time.deltaTime);
        }
    }

    //Called when the player didn't hit perfect, but hits in the safe zone
    public void NotPerfect()
    {
        ConsecutivePerfects = 0;
        AddStepTrigger();
        AnimTriggers.Add("shake");
        StartCoroutine(DoAnimations());
    }

    //Called from the GameController class when it lands on perfect
    public void Perfect()
    {
        ConsecutivePerfects++;
        //Reward the player by increasing the number of steps taken when the user hits perfect
        for (int i = 0; i < ConsecutivePerfects; i++)
        {
            //Add steps
            AddStepTrigger();
        }
        StartCoroutine(DoAnimations());
    }

    public void TakeStep()
    {
        StepNumber++;
        if (StepNumber % 2 == 0)
        {
            Anim.SetTrigger("rightStep");
        }
        else
        {
            Anim.SetTrigger("leftStep");
        }
    }

    public void AddStepTrigger()
    {
        StepNumber++;
        if (StepNumber % 2 == 0)
        {
            AnimTriggers.Add("rightStep");
        }
        else
        {
            AnimTriggers.Add("leftStep");
        }
    }

    // Iterates through the animation triggers and plays each one by one
    IEnumerator DoAnimations()
    {
        Debug.Log("Initiating Animation - Animation Count: " + AnimTriggers.Count);
        while(AnimTriggers.Count > 0)
        {
            Anim.SetTrigger(AnimTriggers[0]);
            Debug.Log(AnimTriggers[0] + ":" + Anim.GetCurrentAnimatorStateInfo(0).length);
            AnimTriggers.RemoveAt(0);
            //If its not the last animation in the sequence, wait out the length of the current animation and call the next one
            yield return new WaitForSeconds(Anim.GetCurrentAnimatorStateInfo(0).length);
        }       
        ResumeSlider();
    }

    //Checks whether the current state is a moving state
    public bool IsMovingState()
    {
        bool moving = false;
        for(int i = 0; i < movingAnimStates.Count; i++)
        {
            var val = Anim.GetCurrentAnimatorStateInfo(0).IsName(movingAnimStates[i]);
            if (val)
            {
                moving = true;
            }
        }
        return moving;
    }

    public void ResumeSlider()
    {
        TouchScript.touch = false;
    }

    public void OnDeath()
    {
        Anim.SetTrigger("death");
    }

    //When player reaches a startpoint, turn direction to the next endpoint (next building)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartPoint")
        {
            MoveToNextPoint = false;
            var startPoints = LevelController.GetStartPoints();
            var endPoints = LevelController.GetEndPoints();
            var index = startPoints.IndexOf(other.gameObject.transform);
            var nextPoint = endPoints[index];
            //Calculate the difference between the two positions to get a directional vector
            var vector = nextPoint.position - other.gameObject.transform.position;
            transform.LookAt(nextPoint);
        }

        if (other.gameObject.tag == "EndPoint")
        {
            var startPoints = LevelController.GetStartPoints();
            var endPoints = LevelController.GetEndPoints();
            var index = endPoints.IndexOf(other.gameObject.transform);
            //Last index
            if(endPoints.Count == index + 1)
            {
                Debug.Log("Game Clear");
            }
            else
            {
                MoveToNextPoint = true;
                var nextPoint = startPoints[index + 1];
                //Calculate the difference between the two positions to get a directional vector
                var vector = nextPoint.position - other.gameObject.transform.position;
                transform.LookAt(nextPoint);
                NextPoint = nextPoint.position;
            }     
        }
    }
}
