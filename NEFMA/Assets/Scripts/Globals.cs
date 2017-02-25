using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player
{
    public string Name;
    public int Number;
    public bool Playing;
    public bool Alive;
    public GameObject Prefab;
    public GameObject GO;
    public Player(string name, int number, bool playing, bool alive, GameObject prefab, GameObject go)
    {
        Name = name;
        Number = number;
        Playing = playing;
        Alive = alive;
        Prefab = prefab;
        GO = go;
    }
}

public class Globals : MonoBehaviour {

    static public int numPlayers = 1;
    static public int livingPlayers = 0;
    static public float musicVolume = 0.5f;
    static public float soundFXVolume = 0.5f;
    static public Checkpoint currentCheckpoint;

    static public Player player1 = new Player("", 0, false, false, null, null); // empty player
    static public Player player2 = new Player("", 1, false, false, null, null); // empty player
    static public Player player3 = new Player("", 2, false, false, null, null); // empty player
    static public Player player4 = new Player("", 3, false, false, null, null); // empty player

    static public bool delilahChosen = false;
    static public bool kittyChosen = false;
    static public bool agniChosen = false;
    static public bool rykerChosen = false;

    // Use this for initialization
    void Start () {
        livingPlayers = 0;
        player1.Playing = true;
        player1.Prefab = Resources.Load("Agni") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (livingPlayers <= 0)
        {
            // should never happen, means that there is no default spawn point
            if (currentCheckpoint == null)
            {
                livingPlayers = numPlayers;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            // the whole team died, respawn them at the last checkpoint
            else
            {
                currentCheckpoint.gameObject.SetActive(true);
                currentCheckpoint.spawning = false;
                currentCheckpoint.resPlayers();
            }
        }
    }
}
