using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;
    public EventSystem eventSys;

    public Sprite agniPause;
    public Sprite delilahPause;
    public Sprite rykerPause;
    public Sprite kittyPause;

    public void pauseGame(int playerInput)
    {
        Time.timeScale = 0;
        Globals.gamePaused = true;
        // open pause Menu
        pauseMenu.SetActive(true);
        eventSys.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal_"+playerInput;
        eventSys.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical_"+playerInput;
        eventSys.GetComponent<StandaloneInputModule>().submitButton = "Select_"+playerInput;
        setPauseBackground(playerInput);
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

    Player getPlayer(int playerInput)
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].InputNum == playerInput)
                return Globals.players[i];
        }
        return null;
    }

    void setPauseBackground(int playerInput)
    {
        Player player = getPlayer(playerInput);

        if (player.Name == "Agni")
            pauseMenu.transform.FindChild("PausePanel").GetComponent<SpriteRenderer>().sprite = agniPause;

        else if (player.Name == "Kitty")
            pauseMenu.transform.FindChild("PausePanel").GetComponent<SpriteRenderer>().sprite = kittyPause;

        else if (player.Name == "Ryker")
            pauseMenu.transform.FindChild("PausePanel").GetComponent<SpriteRenderer>().sprite = rykerPause;

        else if (player.Name == "Delilah")
            pauseMenu.transform.FindChild("PausePanel").GetComponent<SpriteRenderer>().sprite = delilahPause;
    }
}
