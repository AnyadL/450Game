using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script so far just makes the fireballs disappear when they go off screen, more will probably be added later
public class littleFireballScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
 

    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
