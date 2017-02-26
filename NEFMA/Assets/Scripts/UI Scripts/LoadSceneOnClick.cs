using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    private void determinePlayer1()
    {
        Player player;
        int submittingInput = getInputPressed();
        player = new Player("", 0, submittingInput, false, null, null);
        Globals.players.Add(player);
    }

    int getInputPressed()
    {
        if (Input.GetButtonDown("Select_0"))
        {
            return 0;
        }
        else if (Input.GetButtonDown("Select_1"))
        {
            return 1;
        }
        else if (Input.GetButtonDown("Select_2"))
        {
            return 2;
        }
        else if (Input.GetButtonDown("Select_3"))
        {
            return 3;
        }
        else if (Input.GetButtonDown("Select_4"))
        {
            return 4;
        }
        else
        {
            return -1; // no select input was pressed
        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        determinePlayer1();
        SceneManager.LoadScene(sceneIndex);
    }

    
}