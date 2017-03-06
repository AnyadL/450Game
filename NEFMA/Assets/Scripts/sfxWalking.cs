using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxWalking : MonoBehaviour {
    HeroMovement hm;
	// Use this for initialization
	void Start () {
        hm = GetComponent<HeroMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hm.grounded == true && hm.currentSpeed != 0 && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().Play();
        }
	}
}
