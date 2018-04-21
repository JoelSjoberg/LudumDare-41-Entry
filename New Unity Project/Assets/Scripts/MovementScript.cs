using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {


    private string[] stepSounds = { "step1", "step2", "step3" };
    private int stepCounter = 0;
    private int[] sequence = { 0, 1, 0, 2 };
    private int sequenceCounter = 0;

    private RaycastHit hit;
    private int dist;
    private Vector3 dir;
    float lastUpdate = 0;
    int inputCounter = 0;

    enum States{
        idle,
        step,
        step2,
        crouch,
        punch,
        jump
    }

    private States state = States.idle;

    Animator anim;
    AudioController ac;
    SpriteRenderer ren;


	// Use this for initialization
	void Start () {
        ac = FindObjectOfType<AudioSource>().GetComponent<AudioController>();
        anim = GetComponentInChildren<Animator>();
        ren = GetComponentInChildren<SpriteRenderer>();
	}

    // in the future: find a better way to manipulate the animator! This is insane!!!
    // Update is called once per frame
    private float lastInput = 0;
    void Update () {

        anim.SetBool("PressLR", false);
        anim.SetBool("PressLR2", false);
        anim.SetBool("NoInput", false);
        anim.SetBool("Crouch", false);
        anim.SetBool("Punch", false);

        switch (state) {
            case (States.idle):
                idle();
                break;
            case (States.step):
                step();
                break;
            case (States.step2):
                step2();
                break;
            case (States.crouch):
                crouch();
                break;
            case (States.punch):
                punch();
                break;
            default:
                idle();
                break;
        }
        
    }

    float WaitTimer = 0;
    private bool wait(float t)
    {
        WaitTimer += Time.deltaTime;
        if (WaitTimer >= t)
        {
            WaitTimer = 0;
            return false;
        }
        else
        {
            return true;
        }
    }

    private void idle()
    {
        anim.SetBool("NoInput", true);
        if (Input.GetKeyDown(KeyCode.D))
        {
            state = States.step;
            ren.flipX = false;
            transform.position += new Vector3(1, 0, 0).normalized;
            playNext();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = States.step;
            ren.flipX = true;
            transform.position += new Vector3(-1, 0, 0).normalized;
            playNext();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            state = States.crouch;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state = States.punch;
        }
    }

    private void step()
    {
        anim.SetBool("PressLR", true);
        if (Input.GetKeyDown(KeyCode.D))
        {
            state = States.step2;
            ren.flipX = false;
            transform.position += new Vector3(1, 0, 0).normalized;
            playNext();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = States.step2;
            ren.flipX = true;
            transform.position += new Vector3(-1, 0, 0).normalized;
            playNext();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            state = States.crouch;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state = States.punch;
        }
    }

    private void step2()
    {
        anim.SetBool("PressLR2", true);
        if (Input.GetKeyDown(KeyCode.D))
        {
            state = States.step;
            ren.flipX = false;
            transform.position += new Vector3(1, 0, 0).normalized;
            playNext();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = States.step;
            ren.flipX = true;
            transform.position += new Vector3(-1, 0, 0).normalized;
            playNext();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            state = States.crouch;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state = States.punch;
        }
    }

    private void crouch()
    {
        anim.SetBool("Crouch", true);
        if(!wait(0.75f))
        {
            state = States.idle;
        }
    }

    private void punch()
    {
        anim.SetBool("Punch", true);
        if (!wait(0.25f))
        {
            state = States.idle;
        }
    }



    // Play a 8-bit melody as the player moves, the solution is bad but it works.
    public void playNext()
    {
        ac.play(stepSounds[stepCounter]);
        sequenceCounter = (sequenceCounter + 1) % sequence.Length;
        stepCounter = sequence[sequenceCounter];
    }
}
