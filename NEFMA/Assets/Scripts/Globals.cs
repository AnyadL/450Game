using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player
{
    public string Name; // Hero name (Agni)
    public int Number; // Player number (Player 1)
    public int InputNum; // Player input (Horizontal_InputNum)
    public bool Alive; // Whether the player is alive
    public GameObject Prefab; // The hero prefab
    public GameObject GO; // The hero game object
    public Player(string name, int number, int inputnum, bool alive, GameObject prefab, GameObject go)
    {
        Name = name;
        Number = number;
        InputNum = inputnum;
        Alive = alive;
        Prefab = prefab;
        GO = go;
    }

    public override string ToString()
    {
        return "Name: " + Name + " Number: " + Number + " InputNum: " + InputNum + " Alive: " + Alive + " Prefab: " + Prefab + " GO: " + GO;
    }
}

public class Globals : MonoBehaviour {

    static public int numPlayers = 1;
    static public int livingPlayers = 0;
    static public float musicVolume = 0.5f;
    static public float soundFXVolume = 0.5f;
    static public Checkpoint currentCheckpoint;

    static public List<Player> players = new List<Player>();
    
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
