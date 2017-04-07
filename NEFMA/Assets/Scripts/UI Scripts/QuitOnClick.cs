using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitOnClick : MonoBehaviour
{

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void ReturnToMainMenu()
    {
        Globals.gamePaused = false;
        Time.timeScale = 1;
        Globals.players.Clear();
        Globals.numPlayers = 0;
        Globals.livingPlayers = 0;
        Globals.rykerChosen = false;
        Globals.agniChosen = false;
        Globals.delilahChosen = false;
        Globals.kittyChosen = false;
        SceneManager.LoadScene(0);
    }
}