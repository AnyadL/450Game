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
    public int playerNumber;
    public Text playerText;
    public Image playerPanel;
    public Sprite delilahPoster;
    public Sprite agniPoster;
    public Sprite rykerPoster;
    public Sprite kittyPoster;
    public Sprite blankPoster;
    Vector3 delilahPosition;
    Vector3 agniPosition;
    Vector3 rykerPosition;
    Vector3 kittyPosition;
    string delilahText;
    string agniText;
    string rykerText;
    string kittyText;
    [HideInInspector] float nextMovement = 0;
    [HideInInspector] float nextMovementCooldown = 0.25f;

    // Use this for initialization
    void Start () {
        delilahPosition = new Vector3(-topX, topY, 0);
        agniPosition = new Vector3(topX, topY, 0);
        rykerPosition = new Vector3(0, topY, 0);
        kittyPosition = new Vector3(-bottomX, bottomY, 0);

        delilahText = "I may be shy, but don’t let that fool you!  I can summon giant energy hands to protect people.  I can even use my shields to send foes flying!  Is it any wonder I'm the leader of the school’s anti-bullying club?";
        agniText = "I'm a real firebrand, blessed with the super power to manipulate flames. Unfortunately, I'm not so hot at controlling my powers, and my wardrobe has gone up in flames more often than I can count.";
        rykerText = "I'm a very good runner. I can run past my enemies so quickly I completely disorient them.  Someday I'm going to be a supersonic hero.";
        kittyText = "I am the queen of the school improv club. I'm a  social butterfly, and my feline agility makes me the acrobat of the team. In battle, I am “furocious”, favoring a barrage of quick strikes and huge leaps.";

        switch (playerNumber)
        {
            case 0:
                position = new Vector3(-topX, topY, 0);
                playerNumber = Globals.player1.Number;
                print("Player number is " + playerNumber);
                print("Vertical_" + playerNumber);
                break;
            case 1:
                position = new Vector3(0, topY, 0);
                playerNumber = Globals.player2.Number;
                print("Player number is " + playerNumber);
                print("Vertical_" + playerNumber);
                break;

            case 2:
                position = new Vector3(topX, topY, 0);
                playerNumber = Globals.player3.Number;
                print("Player number is " + playerNumber);
                print("Vertical_" + playerNumber);
                break;

            case 3:
                position = new Vector3(-bottomX, bottomY, 0);
                playerNumber = Globals.player4.Number;
                print("Player number is " + playerNumber);
                print("Vertical_" + playerNumber);
                break;
        }
        GetComponent<RectTransform>().localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene(2);
        }
        float vertical = Input.GetAxisRaw("Vertical_" + playerNumber);
        
        if (vertical > 0 && Time.time >= nextMovement)
        {
            nextMovement = Time.time + nextMovementCooldown;
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
        else if (vertical < 0 && Time.time >= nextMovement)
        {
            nextMovement = Time.time + nextMovementCooldown;
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
        float horizontal = Input.GetAxisRaw("Horizontal_" + playerNumber);
        if (horizontal < 0 && Time.time >= nextMovement)
        {
            nextMovement = Time.time + nextMovementCooldown;
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
        else if (horizontal > 0 && Time.time >= nextMovement)
        {
            nextMovement = Time.time + nextMovementCooldown;
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

        setPlayerPanel();
    }

    void setPlayerPanel()
    {
        if (position == rykerPosition)
        {
            playerPanel.sprite = rykerPoster;
            playerText.text = rykerText;
        }
        else if (position == delilahPosition)
        {
            playerPanel.sprite = delilahPoster;
            playerText.text = delilahText;
        }
        else if (position == agniPosition)
        {
            playerPanel.sprite = agniPoster;
            playerText.text = agniText;
        }
        else if (position == kittyPosition)
        {
            playerPanel.sprite = kittyPoster;
            playerText.text = kittyText;
        }
        else
        {
            playerPanel.sprite = blankPoster;
            playerText.text = null;
        }
    }
}

