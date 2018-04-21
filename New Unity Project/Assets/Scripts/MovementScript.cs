using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {


    private string[] stepSounds = { "step1", "step2", "step3" };
    private int stepCounter = 0;
    private int[] sequence = {0, 1, 0, 2};
    private int sequenceCounter = 0;
    float lastUpdate = 0;
    int inputCounter = 0;

    Animator anim;
    AudioController ac;
    SpriteRenderer ren;
    public enum State
    {
        idle,
        walkLeft,
        walkRight,
        jump,
        fall,
        grab,
        attack
    }

	// Use this for initialization
	void Start () {
        ac = FindObjectOfType<AudioSource>().GetComponent<AudioController>();
        anim = GetComponentInChildren<Animator>();
        ren = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        // in the future: find a better way to manipulate the animator! This is insane!!!
        if(lastUpdate == 1)
        {
            anim.SetBool("NoInput", true);
            
            lastUpdate = 0;
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
        if (Input.GetKeyDown(KeyCode.A) && inputCounter == 0)
        {
            ren.flipX = true;
            transform.position += new Vector3(-1, 0, 0).normalized;
            playNext();
            inputCounter++;
            anim.SetBool("PressLR2", false);
            anim.SetBool("PressLR", true);
        }
        else if (Input.GetKeyDown(KeyCode.A) && inputCounter == 1)
        {
            ren.flipX = true;
            transform.position += new Vector3(-1, 0, 0).normalized;
            playNext();
            inputCounter = 0;
            anim.SetBool("PressLR", false);
            anim.SetBool("PressLR2", true);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0, 1, 0).normalized;
            playNext();
        }
        lastUpdate += Time.deltaTime;
    }
    
    // Play a 8-bit melody as the player moves, the solution is bad but it works.
    public void playNext()
    {
        ac.play(stepSounds[stepCounter]);
        sequenceCounter = (sequenceCounter + 1) % 4;
        stepCounter = sequence[sequenceCounter];
    }

    private int walkSequence = 0;

}
