using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour {

    static public int numPlayers = 2;
    static public int livingPlayers = 2;
    static public float musicVolume = 0.5f;
    static public float soundFXVolume = 0.5f;
    static public Checkpoint currentCheckpoint;

    static public GameObject agniPrefab;
    static public GameObject delilahPrefab;

    // Use this for initialization
    void Start () {
        livingPlayers = numPlayers;
        agniPrefab = GameObject.Find("Agni");
        delilahPrefab = GameObject.Find("Delilah");
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
