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
		if (OVERRIDE)
        {
            if (Globals.players.Count == 0)
            {
                Globals.numPlayers = PlayerNumberOverride;
                Globals.livingPlayers = LivingPlayerNumberOverride;
                for (int i = 0; i < PresetPlayers.Count; i++)
                {
                    if (PresetPlayers[i] != null)
                    {
                        Player player = new Player(PresetPlayers[i].name, i, PresetPlayers[i].GetComponent<HeroMovement>().inputNumber, true, (GameObject)Resources.Load(PresetPlayers[i].name), PresetPlayers[i]);
                        //Debug.Log(player);
                        Globals.players.Add(player);
                        PresetPlayers[i].GetComponent<HeroMovement>().playerNumber = i;
                        GameObject canvas = GameObject.Find("HUDCanvas");
                        PresetPlayers[i].GetComponent<SetPlayerUI>().healthSlider = canvas.transform.GetChild(i).FindChild("HealthBar").GetComponent<Slider>();
                        PresetPlayers[i].GetComponent<SetPlayerUI>().powerSlider = canvas.transform.GetChild(i).FindChild("PowerBar").GetComponent<Slider>();
                    }
                }
                GameObject HUDcanvas = GameObject.Find("HUDCanvas");
                HUDcanvas.GetComponent<SetHUDs>().Start();
            }
            else
            {
                for (int i = 0; i < PresetPlayers.Count; i++)
                {
                    if (PresetPlayers[i] != null)
                    {
                        Destroy(PresetPlayers[i]);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit ();
            #endif
        }
        if (pauser)
        {
            for (int i = 0; i <= 4; ++i)
            {
                if (Input.GetButtonDown("Pause_" + i))
                {
                    pauser.pauseGame(i);
                }
            }
        }        
    }
}
