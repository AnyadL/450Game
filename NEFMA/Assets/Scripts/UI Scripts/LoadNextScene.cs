using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private float updateTime = 0f;


    void Update()
    {
        if (updateTime != 0f && Time.time >= updateTime)
        {
            updateTime = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadScene()
    {
        print("Load Scene");
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
            updateTime = Time.time;
        else
        {
            updateTime = Time.time + 1f;
            // fade out
            Globals.gamePaused = false;
            Globals.fading = true;
            Globals.fadeDir = 1;

        }
    }


    
}