using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour {
    private float fadeSpeed = 1f;
    private float alpha = 1.0f;

    public GameObject curtainCall;

    private Color tmpColor;

    void Start()
    {
        // fade in
        Globals.fading = true;
        Globals.fadeDir = -1;
    }

    void Update()
    {
        if (Globals.fading)
        {
            if (curtainCall.GetComponent<Image>().color.a == 0)
            {
                tmpColor = curtainCall.GetComponent<Image>().color;
                tmpColor.a = 1;
                curtainCall.GetComponent<Image>().color = tmpColor;
            }

            alpha += Globals.fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            tmpColor = curtainCall.GetComponent<Image>().color;
                
            tmpColor.a = alpha;

            curtainCall.GetComponent<Image>().color = tmpColor;
            if (alpha == 0)
            {
                Globals.fading = false;
                Globals.bossReset = false;
            }
            else if (alpha == 1 && Globals.fadeDir != -1)
            {
                Globals.fading = false;
                if (SceneManager.GetActiveScene().buildIndex == 7)
                {
                    // Boss just ended, determine which ending they should get
                    int ind = determineEnding();
                    SceneManager.LoadScene(ind);
                }
                else if (SceneManager.GetActiveScene().buildIndex > 7)
                {
                    // End cutscene ended, load credits
                    SceneManager.LoadScene(11);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    private int determineEnding()
    {
        if (Globals.enemiesKilled == 0)
        {
            // no monster abuse
            return 10;
        }
        else if (Globals.enemiesKilled < (Globals.totalEnemies / 2))
        {
            // less than half of enemies killed
            // no excuse
            return 9;
        }
        else
        {
            // monster abuse
            return 8;
        }
    }
}
