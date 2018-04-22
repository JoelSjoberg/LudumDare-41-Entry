using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionTeleport : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("End");
        Debug.Log(name);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
