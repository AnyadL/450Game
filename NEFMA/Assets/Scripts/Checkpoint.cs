using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool spawning = false;
    [HideInInspector] private float aliveTime;
    public float checkpointDuration = 2.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (spawning)
        {
            if (aliveTime <= Time.time)
            {
                gameObject.SetActive(false);
            }
            if (Globals.livingPlayers < Globals.numPlayers)
            {
                resPlayers();
            }
        }
	}

    public void resPlayers()
    {
        Instantiate(Globals.agniPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawning = true;
            aliveTime = Time.time + checkpointDuration;
            Globals.currentCheckpoint = this;
        }
    }
}
