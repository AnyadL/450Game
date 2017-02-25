using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool OVERRIDE = false;
    public int PlayerNumberOverride = 0;
    public int LivingPlayerNumberOverride = 0;

    // Use this for initialization
    void Start () {
		if (OVERRIDE)
        {
            Globals.numPlayers = PlayerNumberOverride;
            Globals.livingPlayers = LivingPlayerNumberOverride;
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
    }
}
