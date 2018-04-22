using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int[] enemyStates = { 0, 0, 0, 0, 0, 0 };
    public Vector3 enemySize = new Vector3(0.2f, 0.2f, 0);

    public Transform[] enemies = new Transform[6];
    int hits = 0;
    public GameStateController gsc;
	// Use this for initialization
	void Start () {
        // Observe! enemies.Length == enemyStates.Length
		for(int i = 0; i < enemyStates.Length; i++)
        {
            enemies[i].localScale = enemySize * enemyStates[i];
        }
	}

    // Update is called once per frame
    private int randomElement;
    void Update() {

        // Do this in real-time
        // Shrink and give points if hit before 4, but only if grown >= 1
        for (int i = 0; i < enemyStates.Length; i++)
        {
            if (enemies[i].GetComponent<ReactToHit>().hit && enemyStates[i] > 0)
            {
                enemies[i].GetComponent<ReactToHit>().hit = false;
                enemyStates[i] = 0;
                enemies[i].localScale = enemySize * enemyStates[i];
                hits++;
                gsc.points++;
            }
        }

        // each allowed excecution
        if (gsc.execute)
        {
            for (int i = 0; i < enemyStates.Length; i++)
            {
                // if grown: keep growing
                if (enemyStates[i] > 0) enemyStates[i]++;
                // If equal to 4: bad for the player
                if (enemyStates[i] == 4)
                {
                    enemyStates[i] = 0;
                    gsc.points--;
                }
                // grow each execution
                enemies[i].localScale = enemySize * enemyStates[i];
            }
            // make random enemy grow, if they haven't grown yet
            randomElement = Random.Range(0, enemyStates.Length);
            if (enemyStates[randomElement] == 0)
            {
                enemyStates[randomElement]++;
                enemies[randomElement].localScale *= enemyStates[randomElement];
            }
        }
        
    }
}