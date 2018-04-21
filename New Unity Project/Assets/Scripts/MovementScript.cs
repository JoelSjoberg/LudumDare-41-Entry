using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public enum State
    {
        walkLeft,
        walkRight,
        jump,
        inAir,
        attack,

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("reg");
            transform.position += new Vector3(1, 0, 0).normalized;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("reg");
            transform.position += new Vector3(-1, 0, 0).normalized;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("reg");
            transform.position += new Vector3(0, 1, 0).normalized;
        }

    }


}
