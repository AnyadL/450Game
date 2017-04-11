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
    public Animator animator;

    void Start ()
    {
		if (initalSpawn)
        {
            Globals.currentCheckpoint = this;
        }
        animator = gameObject.GetComponent<Animator>();
    }

    void Update ()
    {
		if (spawning)
        {
            if (aliveTime <= Time.time)
            {
                //gameObject.SetActive(false);
                spawning = false;
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
                Vector3 myPosition;
                float rand = Random.value;
                float posi = Random.value * 3;
                if (rand > 0.5f)
                {
                    myPosition = transform.position + (Vector3.right * posi);
                }
                else
                {
                    myPosition = transform.position - (Vector3.right * posi);
                }
                GameObject pl = Instantiate(Globals.players[i].Prefab, myPosition, Quaternion.identity);
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
            if (animator != null)
            {
                print("Activated");
                animator.SetBool("Activated", true);
            }
        }
    }
}
