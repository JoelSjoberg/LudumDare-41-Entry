using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {

    // make all event execute at the same time, inputs are stored in a variable and executed when this class allows it.

    public static bool execute = false;
    public int operationsPerSec = 4;
    private float totalTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        totalTime = (1/ operationsPerSec % totalTime == 1 / operationsPerSec) ? (totalTime = 0) : (totalTime += Time.deltaTime);
        execute = totalTime == 0;
	}
}
