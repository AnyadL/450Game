using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public PauseManager pauser;

    public bool OVERRIDE = false;
    public int PlayerNumberOverride = 0;
    public int LivingPlayerNumberOverride = 0;

    public List<GameObject> PresetPlayers = new List<GameObject>();

    // Use this for initialization
    void Start () {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Alive)
            {
                Destroy(Globals.players[i].GO);
                Globals.players[i].GO = null;
                Globals.players[i].Alive = false;
            }
        }
        Globals.livingPlayers = 0;
        Globals.gamePaused = false;
        Time.timeScale = 1;

        Globals.totalEnemies += GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (OVERRIDE)
        {
            if (Globals.players.Count == 0)
            {
                //Debug.Log("Overriding");
                Globals.livingPlayers = LivingPlayerNumberOverride;
                for (int i = 0; i < PresetPlayers.Count; i++)
                {
                    if (PresetPlayers[i] != null)
                    {
                        Player player = new Player(PresetPlayers[i].name, i, PresetPlayers[i].GetComponent<HeroMovement>().inputNumber, true, (GameObject)Resources.Load(PresetPlayers[i].name), PresetPlayers[i]);
                        Globals.players.Add(player);
                        PresetPlayers[i].GetComponent<HeroMovement>().playerNumber = i;
                        GameObject canvas = GameObject.Find("HUDCanvas");
                        PresetPlayers[i].GetComponent<SetPlayerUI>().healthSlider = canvas.transform.GetChild(i).FindChild("HealthBar").GetComponent<Slider>();
                        PresetPlayers[i].GetComponent<SetPlayerUI>().powerSlider = canvas.transform.GetChild(i).FindChild("PowerBar").GetComponent<Slider>();
                        //Debug.Log("Override Ending: " + Globals.players[i]);
                    }
                }
                GameObject HUDcanvas = GameObject.Find("HUDCanvas");
                HUDcanvas.GetComponent<SetHUDs>().Start();
                Globals.bossStart = true;
            }
            else
            {
                for (int i = 0; i < PresetPlayers.Count; i++)
                {
                    //Debug.Log("Override Deleting: " + PresetPlayers[i]);
                    if (PresetPlayers[i] != null)
                    {
                        Destroy(PresetPlayers[i]);
                        //PresetPlayers.RemoveAt(i);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (pauser)
        {
            for (int i = 0; i <= 4; ++i)
            {
                if (Input.GetButtonDown("Pause_" + i))
                {
                    if (pauser.getPlayer(i) == null)
                    {
                        PlayerDropIn(i);
                    }
                    else
                    {
                        pauser.pauseGame(i);
                    }
                }
            }
        }        
    }

    void PlayerDropIn(int playerInput)
    {
        // Player Drop In Code
        int rand = Random.Range(0, 4);
        while (SetHUDs.isNameInUse(SetHUDs.orderedNames[rand]))
        {
            rand = Random.Range(0, 4);
        }

        Player player = new Player("", Globals.players.Count, playerInput, false, null, null);
        Globals.players.Add(player);

        SetHUDs.updatePlayer(player, SetHUDs.orderedNames[rand]);

        GameObject.Find("HUDCanvas").transform.GetChild(Globals.players.Count).FindChild("HealthBar").GetComponent<Slider>().value = 0;
    }
}
