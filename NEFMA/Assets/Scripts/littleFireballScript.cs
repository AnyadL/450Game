using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleFireballScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
 

    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
