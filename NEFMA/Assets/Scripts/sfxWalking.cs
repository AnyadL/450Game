using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxWalking : MonoBehaviour {

    // Use this for initialization
    private HeroMovement hm;
    public AudioSource sfxFootstep;
    public AudioSource sfxJump;
	void Start () {
        hm = GetComponent<HeroMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(hm.grounded == true && hm.currentSpeed != 0 && sfxFootstep.isPlaying == false)
        {
            sfxFootstep.pitch = Random.Range(1.2f, 1.5f);
            sfxFootstep.Play();
        }
        if(hm.jump == true && sfxJump.isPlaying == false)
        {
            sfxJump.pitch = Random.Range(1.0f, 1.3f);
            sfxJump.Play();
        }
	}
}
