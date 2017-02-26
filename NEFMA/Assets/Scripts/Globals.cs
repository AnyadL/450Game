using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player
{
    public string Name; // Hero name (Agni)
    public int Number; // Player number (Player 1)
    public int InputNum; // Player input (Horizontal_InputNum)
    public bool Playing;  // @Dylan TODO: remove this variable (and everything it relates to below)
    public bool Alive; // Whether the player is alive
    public GameObject Prefab; // The hero prefab
    public GameObject GO; // The hero game object
    public Player(string name, int number, int inputnum, bool playing /*remove*/, bool alive, GameObject prefab, GameObject go)
    {
        Name = name;
        Number = number;
        InputNum = inputnum;
        Playing = playing; // remove
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

    static public List<Player> players = new List<Player>();

    // @Dylan TODO: remove these 4 variables (when you can)
    static public Player player1 = new Player("", 0, 0, true, false, null, null); // empty player (there will always be a p1)
    static public Player player2 = new Player("", 1, 1, false, false, null, null); // empty player
    static public Player player3 = new Player("", 2, 2, false, false, null, null); // empty player
    static public Player player4 = new Player("", 3, 3, false, false, null, null); // empty player
    
    static public bool delilahChosen = false;
    static public bool kittyChosen = false;
    static public bool agniChosen = false;
    static public bool rykerChosen = false;

    // Use this for initialization
    void Start () {
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
