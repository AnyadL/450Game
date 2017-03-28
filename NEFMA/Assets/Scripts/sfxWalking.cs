using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxWalking : MonoBehaviour {

    // Use this for initialization
    private HeroMovement hm;
    public AudioSource sfxFootstep;
    public AudioSource sfxJump;

    private bool doublejump = false;
    private bool candoublejump = false;
    private bool isKitty = false;
    private int jumpCount = 0;

    void Start () {
        hm = GetComponent<HeroMovement>();

        if (gameObject.name == "Kitty(Clone)" || gameObject.name == "Kitty")
        {
            isKitty = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //walking sfx
		if(hm.grounded == true && hm.currentSpeed != 0 && sfxFootstep.isPlaying == false)
        {
            sfxFootstep.pitch = Random.Range(1.2f, 1.5f);
            sfxFootstep.Play();
        }
        //jumping sfx
        if(hm.jump == true && sfxJump.isPlaying == false)
        {
            sfxJump.pitch = Random.Range(1.0f, 1.3f);
            sfxJump.Play();
        }
        else if (candoublejump && isKitty && hm.doublejump == true)
        {
            sfxJump.pitch = Random.Range(1.4f, 1.7f);
            sfxJump.Play();
        }
        else if (isKitty && jumpCount == 0 && hm.doublejump == true)
        {
            sfxJump.pitch = Random.Range(1.4f, 1.7f);
            sfxJump.Play();
        }
    }
}
