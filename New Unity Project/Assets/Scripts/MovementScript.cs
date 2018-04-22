using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {


    private string[] stepSounds = { "step1", "step2", "step3" };
    private int stepCounter = 0;
    private int[] sequence = { 0, 1, 0, 2 };
    private int sequenceCounter = 0;

    int inputCounter = 0;
    // save if step was left or right (for turning on place)
    private bool lastStep = true;


    public int movesAllowedToLeft = 0;
    public int movesAllowedToRight = 7;

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
        anim.SetBool("Hit", false);

        if (lastInput >= 1)
        {
            anim.SetBool("NoInput", true);
            state = States.idle;
            lastInput = 0;
        }

        if (!Input.anyKey)
        {
            lastInput += Time.deltaTime;
        }
        else lastInput = 0;

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
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = States.step;
            moveLeft();
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
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = States.step2;
            moveLeft();
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
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = States.step;
            moveLeft();
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

    public void moveLeft()
    {
        ren.flipX = true;
        playNext();
        if (movesAllowedToLeft > 0)
        {
            if (!lastStep)
            {
                transform.position += new Vector3(-1, 0, 0).normalized;
                movesAllowedToRight++;
                movesAllowedToLeft--;
            }
        }
        lastStep = false;
    }

    public void moveRight()
    {
        ren.flipX = false;
        playNext();
        if(movesAllowedToRight > 0)
        {
            if (lastStep)
            {
                transform.position += new Vector3(1, 0, 0).normalized;
                movesAllowedToRight--;
                movesAllowedToLeft++;
            }
        }
        lastStep = true;
    }



    // Play a 8-bit melody as the player moves, the solution is bad but it works.
    public void playNext()
    {
        ac.play(stepSounds[stepCounter]);
        sequenceCounter = (sequenceCounter + 1) % sequence.Length;
        stepCounter = sequence[sequenceCounter];
    }
}
