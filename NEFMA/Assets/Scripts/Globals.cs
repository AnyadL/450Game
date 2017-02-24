using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour {

    static public int numPlayers = 2;
    static public int livingPlayers = 2;
    static public float musicVolume = 0.5f;
    static public float soundFXVolume = 0.5f;
    static public int player1 = 0;
    static public int player2 = 1;
    static public int player3 = 2;
    static public int player4 = 3;
    
	// Use this for initialization
	void Start () {
        livingPlayers = numPlayers;
    }
	
	// Update is called once per frame
	void Update () {
        if (livingPlayers <= 0)
        {
            livingPlayers = numPlayers;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
