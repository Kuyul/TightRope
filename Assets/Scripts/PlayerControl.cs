using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Declare public variables
    public Animator Anim;

    //Declare private variables
    private bool AnimationIsPlaying = false;
    private int StepNumber = 0;
    private int ConsecutivePerfects = 0;
    private List<string> AnimTriggers;

    private void Start()
    {
        AnimTriggers = new List<string>();
    }

    //Called when the player didn't hit perfect, but hits in the safe zone
    public void NotPerfect()
    {
        ConsecutivePerfects = 0;
        AddStepTrigger();
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

    public void AddStepTrigger()
    {
        StepNumber++;
        if (StepNumber % 2 == 0)
        {
            AnimTriggers.Add("rightstep");
        }
        else
        {
            AnimTriggers.Add("leftstep");
        }
    }

    // Iterates through the animation triggers and plays each one by one
    IEnumerator DoAnimations()
    {
        //AnimationIsPlaying = true;
        Debug.Log("Initiating Animation - Animation Count: " + AnimTriggers.Count);
        while(AnimTriggers.Count > 0)
        {
            Anim.SetTrigger(AnimTriggers[0]);
            Debug.Log(AnimTriggers[0] + ":" + Anim.GetCurrentAnimatorStateInfo(0).length);
            AnimTriggers.RemoveAt(0);
            //If its not the last animation in the sequence, wait out the length of the current animation and call the next one
            yield return new WaitForSeconds(Anim.GetCurrentAnimatorStateInfo(0).length);
            //If on the last animation, flag that animations have ended (used by gameController)
            /*
            if (AnimTriggers.Count == 0)
            {
                AnimationIsPlaying = false;
            }*/
        }
    }

    //A simple getter (is this still a good coding methodology? why not just use a public variable instead? feels outdated.. its been 7 years already, but nevertheless it works)
    public bool IsAnimationPlaying()
    {
        return AnimationIsPlaying;
    }
}
