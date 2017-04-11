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
    public int Score; //This is the heros score (number of collectibles
    public UnityEngine.UI.Text ScoreCounter;
    public int Deaths; // This is the number of times the player has died

    public Player(string name, int number, int inputnum, bool alive, GameObject prefab, GameObject go,int score=0, UnityEngine.UI.Text scoreCounter =null, int deaths=0)
    {
        Name = name;
        Number = number;
        InputNum = inputnum;
        Alive = alive;
        Prefab = prefab;
        GO = go;
        Score = score;
        ScoreCounter = scoreCounter;
        Deaths = deaths;
    }

    public override string ToString()
    {
        return "Name: " + Name + "| Number: " + Number + "| InputNum: " + InputNum + "| Alive: " + Alive + "| Prefab: " + Prefab + "| GO: " + GO;
    }
}

public class Globals
{
    static public int numPlayers = 0;
    static public int livingPlayers = 0;
    static public float musicVolume = 0.5f;
    static public float soundFXVolume = 0.5f;
    static public bool gamePaused = false;
    static public Checkpoint currentCheckpoint;

    static public List<Player> players = new List<Player>();

    static public bool delilahChosen = false;
    static public bool kittyChosen = false;
    static public bool agniChosen = false;
    static public bool rykerChosen = false;

    static public int enemiesKilled = 0;
    static public int totalEnemies = 0;

    static public float fadeDir = 1f;
    static public bool fading = false;

    static public bool bossReset = false;
    static public bool bossStart = false;
}

public class GlobalContainer : MonoBehaviour {

    public bool BOSSLEVEL = false;
    //public bool bossReset = false;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (Globals.livingPlayers <= 0)
        {
            // should never happen, means that there is no default spawn point
            if (Globals.currentCheckpoint == null)
            {
                Debug.Log("COULD NOT FIND CURRENT CHECKPOINT");
            }
            // the whole team died, respawn them at the last checkpoint
            else if (!BOSSLEVEL)
            {
                //Globals.currentCheckpoint.gameObject.SetActive(true);
                //Globals.currentCheckpoint.spawning = false;
                //Debug.Log("Globals Ressing Players");
                Globals.currentCheckpoint.resPlayers();
                return;
            }
            else if (!Globals.bossStart)
            {
                Globals.bossStart = true;
                //Globals.currentCheckpoint.spawning = false;
                Globals.currentCheckpoint.resPlayers();
            }
            else if (!Globals.bossReset)
            {
                Globals.bossReset = true;
                Globals.bossStart = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }
        }
    }
}
