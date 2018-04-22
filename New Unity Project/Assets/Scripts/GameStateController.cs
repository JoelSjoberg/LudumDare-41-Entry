using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {

    // make all event execute at the same time, inputs are stored in a variable and executed when this class allows it.

    public bool execute = false;
    public float operationsFreq = 1;
    private float totalTime = 0;

    public int points = 0;
    public static int sPoints;

    private string[] stepSounds = { "e1", "e2", "e3" , "e4"};
    private int stepCounter = 0;
    private int[] sequence = { 0, 1, 2, 3};
    private int sequenceCounter = 0;
    public int goal = 50;
    public static int sGoal;
    public bool finished = false;
    public Transform teleportCube;

    AudioController ac;

	// Use this for initialization
	void Start () {
        ac = FindObjectOfType<AudioSource>().GetComponent<AudioController>();
        sGoal = goal;
    }
	
	// Update is called once per frame
	void Update () {
        // player just lost a point
        if (sPoints > points) ac.play("fail");

        sPoints = points;

        if (sPoints == goal && !finished)
        {
            ac.play("Victory");
            finished = true;
            execute = false;
            teleportCube.position = new Vector3(teleportCube.position.x, teleportCube.position.y, -1);
        }
        if (!finished)
        {
            if(totalTime >= 1 / operationsFreq)
            {
                totalTime = 0;
                execute = true;
                playNext();
            }
            else
            {
                totalTime += Time.deltaTime;
                execute = false;
            }
            if (points > 10) {
                operationsFreq = 1.2f;
            }
            if (points > 25)
            {
                operationsFreq = 1.5f;
            }
            if (points > 40) {
                operationsFreq = 1.7f;
            }
            if (points > 60)
            {
                operationsFreq = 2f;
            }
            if (points > 70)
            {
                operationsFreq = 2.2f;
            }
        }
    }
    public void playNext()
    {
        ac.play(stepSounds[stepCounter]);
        sequenceCounter = (sequenceCounter + 1) % sequence.Length;
        stepCounter = sequence[sequenceCounter];
    }
}
