using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStamina : MonoBehaviour {


    public MovementScript player;
    public Text st;
	// Use this for initialization
	void Start () {
        st = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        st.text = "Stamina: " + player.stamina;
	}
}
