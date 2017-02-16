using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onSoundLoad : MonoBehaviour {

    public AudioSource sound;

	// Use this for initialization
	void Start () {
        if (sound.tag == "Music")
            sound.volume = Globals.musicVolume;
        else if (sound.tag == "SoundFX")
            sound.volume = Globals.soundFXVolume;
        else
            print("There is an untagged sound. Please tag your sounds as Music or SoundFX. Sound:" + sound.name);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
