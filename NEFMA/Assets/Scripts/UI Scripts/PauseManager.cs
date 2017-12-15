using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject pauseMenu;
    public EventSystem eventSys;
    public Selectable resumeButton;

    public Sprite agniPause;
    public Sprite delilahPause;
    public Sprite rykerPause;
    public Sprite kittyPause;

    private Image pauseImage;

    int _pausedPlayer = 0;

    void Start()
    {
        pauseImage = pauseMenu.transform.FindChild("PausePanel").GetComponent<Image>();
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
        setPauseBackground(playerInput);
        resumeButton.OnSelect(null);
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

    public void restartLevel()
    {
        //Debug.Log("Calling Restart Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public Player getPlayer(int playerInput)
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].InputNum == playerInput)
                return Globals.players[i];
        }
        return null;
    }
    
    public void PlayerDropOut()
    {
        // Player Drop Out Code
        Debug.Log("Player Dropped Out");
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            Player player = Globals.players[i];
            Debug.Log(player + " --- " + _pausedPlayer);
            if (player.InputNum == _pausedPlayer)
            {
                if (player.GO != null)
                {
                    Destroy(player.GO);
                }
                Globals.players.Remove(player);
                for (int j = i; j < Globals.players.Count; j++)
                {
                    Globals.players[i].Number = i;
                }
                break;
            }
        }
        Debug.Log("Global List Now:");
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            Debug.Log(Globals.players[i]);
        }
        playGame();
    }

    void setPauseBackground(int playerInput)
    {
        _pausedPlayer = playerInput;
        Player player = getPlayer(_pausedPlayer);
        if (player != null)
        {
            if (player.Name == "Agni")
                pauseImage.sprite = agniPause;

            else if (player.Name == "Kitty")
                pauseImage.sprite = kittyPause;

            else if (player.Name == "Ryker")
                pauseImage.sprite = rykerPause;

            else if (player.Name == "Delilah")
                pauseImage.sprite = delilahPause;
        }
        else
        {
            pauseImage.sprite = agniPause;
        }
    }
}
