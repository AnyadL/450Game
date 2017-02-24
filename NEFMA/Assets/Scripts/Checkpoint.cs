using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool active = false;
    [HideInInspector] private float aliveTime;
    public float checkpointDuration = 2.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (active)
        {
            if (aliveTime <= Time.time)
            {
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "Player")
        {
            active = true;
            aliveTime = Time.time + checkpointDuration;
        }
    }
}
