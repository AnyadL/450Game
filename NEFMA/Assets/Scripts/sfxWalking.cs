using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxWalking : MonoBehaviour {

    // Use this for initialization
    private HeroMovement hm;
    public AudioSource sfxFootstep;
	void Start () {
        hm = GetComponent<HeroMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hm.grounded == true && hm.currentSpeed != 0 && sfxFootstep.isPlaying == false)
        {
            sfxFootstep.Play();
        }
	}
}
