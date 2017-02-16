using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

    static public int numPlayers = 2;
    static public int livingPlayers = 2;
    static public float musicVolume = 0.5f;
    static public float soundFXVolume = 0.5f;

	// Use this for initialization
	void Start () {
        livingPlayers = numPlayers; // doesnt do anything, function not called
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
