using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;
    public EventSystem eventSys;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void pauseGame(int playerInput)
    {
        Time.timeScale = 0;
        Globals.gamePaused = true;
        // open pause Menu
        pauseMenu.SetActive(true);
        eventSys.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal_"+playerInput;
        eventSys.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical_"+playerInput;
        eventSys.GetComponent<StandaloneInputModule>().submitButton = "Select_"+playerInput;
    }

    public void playGame()
    {
        Time.timeScale = 1;
        Globals.gamePaused = false;
        // close pause Menu
        pauseMenu.SetActive(false);
        eventSys.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal_-1";
        eventSys.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical_-1";
        eventSys.GetComponent<StandaloneInputModule>().submitButton = "Submit";
    }
}
