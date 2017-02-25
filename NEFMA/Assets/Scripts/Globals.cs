using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player
{
    public string Name;
    public int Number;
    public bool Playing;
    public Player(string name, int number, bool playing)
    {
        Name = name;
        Number = number;
        Playing = playing;
    }
}

public class Globals : MonoBehaviour {

    static public int numPlayers = 2;
    static public int livingPlayers = 2;
    static public float musicVolume = 0.5f;
    static public float soundFXVolume = 0.5f;
    static public Checkpoint currentCheckpoint;

    static public GameObject agniPrefab;
    static public GameObject delilahPrefab;

    static public Player player1 = new Player("Agni", 0, true);
    static public Player player2 = new Player("", 1, false); // empty player
    static public Player player3 = new Player("", 2, false); // empty player
    static public Player player4 = new Player("", 3, false); // empty player

    static public bool delilahChosen = false;
    static public bool kittyChosen = false;
    static public bool agniChosen = false;
    static public bool rykerChosen = false;

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
