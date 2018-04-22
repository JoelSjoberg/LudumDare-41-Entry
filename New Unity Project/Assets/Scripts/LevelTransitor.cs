using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitor : MonoBehaviour {

    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = destination.position;
    }
}
