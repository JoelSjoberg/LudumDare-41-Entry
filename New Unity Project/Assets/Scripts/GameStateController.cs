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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        sPoints = points;
        if(totalTime >= 1 / operationsFreq)
        {
            totalTime = 0;
            execute = true;
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
