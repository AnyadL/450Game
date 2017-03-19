using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

    public bool spawning = false;
    [HideInInspector] private float aliveTime;
    public float checkpointDuration = 2.0f;
    public bool initalSpawn = false;

    void Start ()
    {
		if (initalSpawn)
        {
            Globals.currentCheckpoint = this;
        }
    }

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

    // respawn all of the dead players at the current gameobject
    public void resPlayers()
    {
        for (int i = 0; i < Globals.players.Count; i++)
        {
            if (!Globals.players[i].Alive)
            {
                //Debug.Log("Checkpoint Starting: " + Globals.players[i]);
                GameObject pl = Instantiate(Globals.players[i].Prefab, transform.position, Quaternion.identity);
                Globals.players[i].Alive = true;
                Globals.players[i].GO = pl;
                pl.GetComponent<HeroMovement>().playerNumber = Globals.players[i].Number;
                pl.GetComponent<HeroMovement>().inputNumber = Globals.players[i].InputNum;
                GameObject canvas = GameObject.Find("HUDCanvas");
                pl.GetComponent<SetPlayerUI>().healthSlider = canvas.transform.GetChild(i).FindChild("HealthBar").GetComponent<Slider>();
                pl.GetComponent<SetPlayerUI>().powerSlider = canvas.transform.GetChild(i).FindChild("PowerBar").GetComponent<Slider>();
                pl.GetComponent<AttributeController>().myLayer = pl.gameObject.layer;
                if (!initalSpawn)
                {
                    pl.GetComponent<AttributeController>().takenDamage();
                }
                //Debug.Log("Checkpoint Ending: " + Globals.players[i]);
            }
        }
        Globals.livingPlayers = Globals.players.Count;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if we collided with a player and we are not currently spawning
        if (other.gameObject.tag == "Player" && !spawning)
        {
            spawning = true;
            aliveTime = Time.time + checkpointDuration;
            Globals.currentCheckpoint = this;
        }
    }
}
