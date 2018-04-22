using UnityEngine;
using UnityEngine.UI;

public class ShowPoints : MonoBehaviour {

    public Text t;
	// Use this for initialization
	void Start () {
        t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = ""+GameStateController.sPoints + "/" + GameStateController.sGoal;
	}
}
