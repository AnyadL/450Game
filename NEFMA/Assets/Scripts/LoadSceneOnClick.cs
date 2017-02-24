using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    private void determinePlayer1()
    {
        if (!(Input.GetKeyDown("return") || Input.GetKeyDown("enter") || Input.GetKeyDown("space")))
        {
            // Player 1 is controller
            Globals.player1 = 1;
            Globals.player2 = 2;
            Globals.player3 = 3;
            Globals.player4 = 4;
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        determinePlayer1();
        SceneManager.LoadScene(sceneIndex);
    }

    
}