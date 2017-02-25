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
    public Image playerSelector;
    public Sprite playerSelectorImage;
    public Sprite playerSelectedImage;
    public Sprite hiddenSelector;

    public Sprite delilahPoster;
    public Sprite agniPoster;
    public Sprite rykerPoster;
    public Sprite kittyPoster;
    public Sprite blankPoster;
    public Sprite startPoster;

    Vector3 delilahPosition;
    Vector3 agniPosition;
    Vector3 rykerPosition;
    Vector3 kittyPosition;

    string delilahText;
    string agniText;
    string rykerText;
    string kittyText;

    private bool heroSelected = false;

    [HideInInspector] float nextMovement = 0;
    [HideInInspector] float nextMovementCooldown = 0.25f;
    private bool allPlayersSelected = false;

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
                position = delilahPosition;
                playerNumber = Globals.player1.Number;
                break;
            case 1:
                position = rykerPosition;
                playerNumber = Globals.player2.Number;
                playerSelector.sprite = hiddenSelector;
                break;

            case 2:
                position = agniPosition;
                playerNumber = Globals.player3.Number;
                playerSelector.sprite = hiddenSelector;
                break;

            case 3:
                position = kittyPosition;
                playerNumber = Globals.player4.Number;
                playerSelector.sprite = hiddenSelector;
                break;
            
        }
        GetComponent<RectTransform>().localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && allPlayersSelected)
        {
            SceneManager.LoadScene(2);
        }

        // check if select button (A or enter) was pressed
        if (Input.GetButtonDown("Select_" + playerNumber))
        {
            selectPressed();
        }

        if (!heroSelected)
        {
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
    }

    void setPlayerPanel()
    {
        if (playerSelector.sprite == hiddenSelector)
            playerPanel.sprite = startPoster;
        else
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

    string testPosition()
    {
        if (position == rykerPosition && !Globals.rykerChosen)
        {
            return "Ryker";
        }
        else if (position == delilahPosition && !Globals.delilahChosen)
        {
            return "Delilah";
        }
        else if (position == agniPosition && !Globals.agniChosen)
        {
            return "Agni";
        }
        else if (position == kittyPosition && !Globals.kittyChosen)
        {
            return "Kitty";
        }
        else
        {
            return "null";
        }
    }

    void selectPressed ()
    {
        joinOrSelectHero();
    }

    void joinOrSelectHero ()
    {
        int player = playerNumber;
        if (Globals.player1.Number == 1)
            player = playerNumber - 1;

        switch (player)
        {
            case 0:
                if (Globals.player1.Playing == true)
                {
                    // set player hero selection
                    string hero = testPosition();
                    if (hero != "null")
                        setHero(player, hero);
                }
                break;
            case 1:
                if (Globals.player2.Playing == true)
                {
                    // set player hero selection
                    string hero = testPosition();
                    if (hero != "null")
                        setHero(player, hero);
                }
                else
                {
                    // join game
                    playerSelector.sprite = playerSelectorImage;
                    playerPanel.sprite = blankPoster;
                    Globals.player2.Playing = true;
                }
                break;

            case 2:
                if (Globals.player3.Playing == true)
                {
                    // set player hero selection
                    string hero = testPosition();
                    if (hero != "null")
                        setHero(player, hero);
                }
                else
                {
                    // join game
                    playerSelector.sprite = playerSelectorImage;
                    playerPanel.sprite = blankPoster;
                    Globals.player3.Playing = true;
                }

                break;

            case 3:
                if (Globals.player3.Playing == true)
                {
                    // set player hero selection
                    string hero = testPosition();
                    if (hero != "null")
                        setHero(player, hero);
                }
                else
                {
                    // join game
                    playerSelector.sprite = playerSelectorImage;
                    playerPanel.sprite = blankPoster;
                    Globals.player4.Playing = true;
                }

                break;
        }
    }

    void setHero (int player, string hero)
    {
        print("In set hero");
        if (!heroSelected)
        {
            print("hero not selected");
            bool heroChoiceAllowed = true;

            switch (hero)
            {
                case "Ryker":
                    if (Globals.rykerChosen)
                        heroChoiceAllowed = false;
                    Globals.rykerChosen = true;
                    break;
                case "Delilah":
                    if (Globals.delilahChosen)
                        heroChoiceAllowed = false;
                    Globals.delilahChosen = true;
                    break;
                case "Kitty":
                    if (Globals.kittyChosen)
                        heroChoiceAllowed = false;
                    Globals.kittyChosen = true;
                    break;
                case "Agni":
                    if (Globals.agniChosen)
                        heroChoiceAllowed = false;
                    Globals.agniChosen = true;
                    break;
            }
            if (heroChoiceAllowed)
            {
                print("hero allowed");
                switch (player)
                {
                    case 0:
                        Globals.player1.Name = hero;
                        Debug.Log("Player 1 chose " + hero);
                        break;
                    case 1:
                        Globals.player2.Name = hero;
                        Debug.Log("Player 2 chose " + hero);
                        break;

                    case 2:
                        Globals.player3.Name = hero;
                        Debug.Log("Player 3 chose " + hero);
                        break;

                    case 3:
                        Globals.player4.Name = hero;
                        Debug.Log("Player 4 chose " + hero);
                        break;
                }
                heroSelected = true;
                playerSelector.sprite = playerSelectedImage;
            }
        }
    }

}

