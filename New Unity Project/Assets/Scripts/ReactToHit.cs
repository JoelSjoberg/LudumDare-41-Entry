using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToHit : MonoBehaviour {

    public bool hit = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Player" && MovementScript.state == MovementScript.States.hit)
        {
            hit = true;
        }
    }
}
