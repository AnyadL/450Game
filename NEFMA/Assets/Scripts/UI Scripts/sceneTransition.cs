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
            }
            else if (alpha == 1)
            {
                Globals.fading = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
