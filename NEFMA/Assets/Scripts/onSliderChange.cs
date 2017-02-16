using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class onSliderChange : MonoBehaviour {

    public Slider slider;
    public bool music;
    private AudioSource[] sounds;

    void Start ()
    {
        sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    public void changeVolume ()
    {
        for(int i = 0; i < sounds.Length; ++i)
        {
            // Check for untagged sounds
            if ((sounds[i].tag != "SoundFX") && (sounds[i].tag != "Music"))
            {
                print("There is an untagged sound. Please tag your sounds as Music or SoundFX. Sound:" + sounds[i].name);
            }

            // If this is the music slider, change music volume
            if (music && (sounds[i].tag == "Music"))
            {
                sounds[i].volume = slider.value;
            }
            // If this is the sound effects slider, change sound effects volume
            else if (!music && (sounds[i].tag == "SoundFX"))
            {
                sounds[i].volume = slider.value;
            }
        }

        // change globals
        if (music)
        {
            Globals.musicVolume = slider.value;
        }
        else
        {
            Globals.soundFXVolume = slider.value;
        }
    }
}
