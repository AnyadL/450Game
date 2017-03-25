using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	void returnToMainMenu()
    {
        Globals.gamePaused = false;
        Time.timeScale = 1;
        Globals.players.Clear();
        Globals.numPlayers = 0;
        Globals.livingPlayers = 0;
        SceneManager.LoadScene(0);
    }
}
