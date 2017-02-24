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


    static public Player player1 = new Player("Agni", 0, true);
    static public Player player2 = new Player("", 1, false); // empty player
    static public Player player3 = new Player("", 2, false); // empty player
    static public Player player4 = new Player("", 3, false); // empty player

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
