using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool spawning = false;
    [HideInInspector] private float aliveTime;
    public float checkpointDuration = 2.0f;
    public bool initalSpawn = false;

    // Use this for initialization
    void Start ()
    {
		if (initalSpawn)
        {
            Globals.currentCheckpoint = this;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (spawning)
        {
            if (aliveTime <= Time.time)
            {
                gameObject.SetActive(false);
            }
            if (Globals.livingPlayers < Globals.numPlayers)
            {
                resPlayers();
            }
        }
	}

    public void resPlayers()
    {
        if (Globals.player1.Playing && !Globals.player1.Alive)
        {
            GameObject pl1 = Instantiate(Globals.player1.Prefab, transform.position, Quaternion.identity);
            Globals.player1.Alive = true;
            Globals.player1.GO = pl1;
            pl1.GetComponent<HeroMovement>().playerNumber = Globals.player1.Number.ToString();
            pl1.GetComponent<AttributeController>().myLayer = pl1.gameObject.layer;
            pl1.GetComponent<AttributeController>().takenDamage();
        }
        if (Globals.player2.Playing && !Globals.player2.Alive)
        {
            GameObject pl2 = Instantiate(Globals.player2.Prefab, transform.position, Quaternion.identity);
            Globals.player2.Alive = true;
            Globals.player2.GO = pl2;
            pl2.GetComponent<HeroMovement>().playerNumber = Globals.player2.Number.ToString();
            pl2.GetComponent<AttributeController>().myLayer = pl2.gameObject.layer;
            pl2.GetComponent<AttributeController>().takenDamage();
        }
        if (Globals.player3.Playing && !Globals.player3.Alive)
        {
            GameObject pl3 = Instantiate(Globals.player3.Prefab, transform.position, Quaternion.identity);
            Globals.player3.Alive = true;
            Globals.player3.GO = pl3;
            pl3.GetComponent<HeroMovement>().playerNumber = Globals.player3.Number.ToString();
            pl3.GetComponent<AttributeController>().myLayer = pl3.gameObject.layer;
            pl3.GetComponent<AttributeController>().takenDamage();
        }
        if (Globals.player4.Playing && !Globals.player4.Alive)
        {
            GameObject pl4 = Instantiate(Globals.player4.Prefab, transform.position, Quaternion.identity);
            Globals.player4.Alive = true;
            Globals.player4.GO = pl4;
            pl4.GetComponent<HeroMovement>().playerNumber = Globals.player4.Number.ToString();
            pl4.GetComponent<AttributeController>().myLayer = pl4.gameObject.layer;
            pl4.GetComponent<AttributeController>().takenDamage();
        }
        Globals.livingPlayers = Globals.numPlayers;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !spawning)
        {
            spawning = true;
            aliveTime = Time.time + checkpointDuration;
            Globals.currentCheckpoint = this;
        }
    }
}
