using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {


    private string[] stepSounds = { "step1", "step2", "step3" };
    private int stepCounter = 0;
    private int[] sequence = {0, 1, 0, 2};
    private int sequenceCounter = 0;

    private RaycastHit hit;
    private int dist;
    private Vector3 dir;
    float lastUpdate = 0;
    int inputCounter = 0;

    public enum States {
        idle, 
        step,
        crouch
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
        
        switch(state) {
            case (States.idle):
                idle();
                break;

            default:
                idle();
                break;
        }
        lastInput += Time.deltaTime;
            if (lastInput > 1)
            {
                anim.SetBool("PressLR", false);
                anim.SetBool("PressLR2", false);
                anim.SetBool("NoInput", true);
            }
            if (Input.anyKey){
                lastInput = 0;
                anim.SetBool("NoInput", false);
            }
            if (Input.GetKeyDown(KeyCode.D) && inputCounter == 0)
            {
                ren.flipX = false;
                transform.position += new Vector3(1, 0, 0).normalized;
                playNext();
                inputCounter++;
                anim.SetBool("PressLR2", false);
                anim.SetBool("PressLR", true);
            }
            else if (Input.GetKeyDown(KeyCode.D) && inputCounter == 1)
            {
                ren.flipX = false;
                transform.position += new Vector3(1, 0, 0).normalized;
                playNext();
                inputCounter = 0;
                anim.SetBool("PressLR", false);
                anim.SetBool("PressLR2", true);
            }
            if (Input.GetKeyDown(KeyCode.A) && inputCounter == 1)
            {
                ren.flipX = true;
                transform.position += new Vector3(-1, 0, 0).normalized;
                playNext();
                inputCounter = 0;
                anim.SetBool("PressLR2", false);
                anim.SetBool("PressLR", true);
            }
            else if (Input.GetKeyDown(KeyCode.A) && inputCounter == 0)
            {
                ren.flipX = true;
                transform.position += new Vector3(-1, 0, 0).normalized;
                playNext();
                inputCounter++;
                anim.SetBool("PressLR", false);
                anim.SetBool("PressLR2", true);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetBool("Crouch", true);
                
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
            ren.flipX = false;
            transform.position += new Vector3(1, 0, 0).normalized;
            playNext();
            inputCounter++;
            States = 
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
