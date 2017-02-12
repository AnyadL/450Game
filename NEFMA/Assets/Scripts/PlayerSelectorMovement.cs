using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectorMovement : MonoBehaviour {
    
    Vector3 position;
    int topX = 400;
    int bottomX = 600;
    int topY = 70;
    int bottomY = -50;
    public int player;

    // Use this for initialization
    void Start () {
        switch(player)
        {
            case 0:
                position = new Vector3(-topX, topY, 0);
                break;
            case 1:
                position = new Vector3(0, topY, 0);
                break;

            case 2:
                position = new Vector3(topX, topY, 0);
                break;

            case 3:
                position = new Vector3(-bottomX, bottomY, 0);
                break;
        }
        GetComponent<RectTransform>().localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == 0)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene(2);
            }
            if (Input.GetKeyDown("up"))
            {
                if (!(GetComponent<RectTransform>().localPosition.y == topY))
                {
                    position.y = topY;
                    if (position.x == bottomX)
                        position.x = topX;
                    else if (position.x == -bottomX)
                        position.x = -topX;

                    GetComponent<RectTransform>().localPosition = position;
                }
            }
            else if (Input.GetKeyDown("down"))
            {
                if (!(GetComponent<RectTransform>().localPosition.y == bottomY))
                {
                    position.y = bottomY;
                    if (position.x == topX)
                        position.x = bottomX;
                    else if (position.x == -topX)
                        position.x = -bottomX;

                    GetComponent<RectTransform>().localPosition = position;
                }
            }
            else if (Input.GetKeyDown("left"))
            {
                if ((GetComponent<RectTransform>().localPosition.y == topY))
                {
                    if (!(GetComponent<RectTransform>().localPosition.x == -topX))
                    {
                        position.x = GetComponent<RectTransform>().localPosition.x - topX;
                        GetComponent<RectTransform>().localPosition = position;
                    }
                }
                else
                {
                    if (!(GetComponent<RectTransform>().localPosition.x == -bottomX))
                    {
                        position.x = GetComponent<RectTransform>().localPosition.x - bottomX;
                        GetComponent<RectTransform>().localPosition = position;
                    }
                }
            }
            else if (Input.GetKeyDown("right"))
            {
                if ((GetComponent<RectTransform>().localPosition.y == topY))
                {
                    if (!(GetComponent<RectTransform>().localPosition.x == topX))
                    {
                        position.x = GetComponent<RectTransform>().localPosition.x + topX;
                        GetComponent<RectTransform>().localPosition = position;
                    }
                }
                else
                {
                    if (!(GetComponent<RectTransform>().localPosition.x == bottomX))
                    {
                        position.x = GetComponent<RectTransform>().localPosition.x + bottomX;
                        GetComponent<RectTransform>().localPosition = position;
                    }
                }
            }
        }
    }
}

// (-270,70) (0,70) (270,70)
// (-320,-50) (0,-50) (320,-50)