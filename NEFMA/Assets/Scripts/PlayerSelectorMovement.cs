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
    public Sprite playerSelectorSprite;
    public Sprite playerSelectedSprite;
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

    string delilahText = "I may be shy, but don’t let that fool you!  I can summon giant energy hands to protect people.  I can even use my shields to send foes flying!  Is it any wonder I'm the leader of the school’s anti-bullying club?";
    string agniText = "I'm a real firebrand, blessed with the super power to manipulate flames. Unfortunately, I'm not so hot at controlling my powers, and my wardrobe has gone up in flames more often than I can count.";
    string rykerText = "I'm a very good runner. I can run past my enemies so quickly I completely disorient them.  Someday I'm going to be a supersonic hero.";
    string kittyText = "I am the queen of the school improv club. I'm a  social butterfly, and my feline agility makes me the acrobat of the team. In battle, I am “furocious”, favoring a barrage of quick strikes and huge leaps.";

    private bool heroSelected = false;
    private bool joinedGame = false;
    private int playerInput; // assigned based on which controller this player registers with
    private int inputPressed; // assigned based on which controller pressed a button
    private Player player;

    [HideInInspector] float nextMovement = 0;
    [HideInInspector] float nextMovementCooldown = 0.25f;

    // Use this for initialization
    void Start () {
        position = new Vector3(0, topY, 0);
        delilahPosition = new Vector3(-topX, topY, 0);
        agniPosition = new Vector3(topX, topY, 0);
        rykerPosition = new Vector3(0, topY, 0);
        kittyPosition = new Vector3(-bottomX, bottomY, 0);
        
        if (playerNumber == 0)
        {
            // Create player 1
            if (Globals.players.Count == 0)
            {
                player = new Player("", 1, 0, false, null, null);
                Globals.players.Add(player);
            }
            playerInput = Globals.players[0].InputNum;
            joinedGame = true;
            playerSelector.sprite = playerSelectorSprite;
        }
        else
            playerSelector.sprite = hiddenSelector;

        GetComponent<RectTransform>().localPosition = position;
    }

    // Update is called once per frame
    void Update()
    {

        // check if select button (A or enter) was pressed
        inputPressed = getInputPressed();
        if (inputPressed != -1)
        {
            selectPressed();
        }

        if (!heroSelected && joinedGame)
        {
            float vertical = Input.GetAxisRaw("Vertical_" + playerInput);

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
            float horizontal = Input.GetAxisRaw("Horizontal_" + playerInput);
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
        {
            playerPanel.sprite = startPoster;
            playerText.text = null;
        }
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
            Globals.rykerChosen = true;
            return "Ryker";
        }
        else if (position == delilahPosition && !Globals.delilahChosen)
        {
            Globals.delilahChosen = true;
            return "Delilah";
        }
        else if (position == agniPosition && !Globals.agniChosen)
        {
            Globals.agniChosen = true;
            return "Agni";
        }
        else if (position == kittyPosition && !Globals.kittyChosen)
        {
            Globals.kittyChosen = true;
            return "Kitty";
        }
        else
        {
            return "null";
        }
    }

    void selectPressed ()
    {

        if(heroSelected && (inputPressed == playerInput))
        {
            if (haveAllPlayersSelected())
                SceneManager.LoadScene(2);
        }
        else
            joinOrSelectHero();
    }

    void joinOrSelectHero ()
    {
        if (joinedGame && (inputPressed == playerInput)) // select Hero
        {
            string hero = testPosition();
            if (hero != "null")
                setHero(hero);
        }
        else // join
        {
            if (!joinedGame && !isInputInUse())
            {
                playerInput = inputPressed;
                player = new Player("", playerNumber, playerInput, false, null, null);
                Globals.players.Add(player);

                joinedGame = true;
                playerSelector.sprite = playerSelectorSprite;
                playerPanel.sprite = blankPoster;
                ++Globals.numPlayers;

            }
        }
    }

    bool isInputInUse()
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].InputNum == inputPressed)
                return true;
        }
        return false;
    }

    int getInputPressed()
    {
        if (Input.GetButtonUp("Select_0"))
        {
            return 0;
        }
        else if (Input.GetButtonUp("Select_1"))
        {
            return 1;
        }
        else if (Input.GetButtonUp("Select_2"))
        {
            return 2;
        }
        else if (Input.GetButtonUp("Select_3"))
        {
            return 3;
        }
        else if (Input.GetButtonUp("Select_4"))
        {
            return 4;
        }
        else
        {
            return -1; // no select input was pressed
        }
    }

    void setHero (string hero)
    {
        if (!heroSelected)
        {
            bool heroChoiceAllowed = true;

          /*  switch (hero)
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
            }*/
            if (heroChoiceAllowed)
            {
                Globals.players[playerNumber].Name = hero;
                Globals.players[playerNumber].Prefab = Resources.Load(hero) as GameObject;

                heroSelected = true;
                playerSelector.sprite = playerSelectedSprite;
            }
        }
    }

    bool haveAllPlayersSelected()
    {
        int namedCount = 0;
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Name != "")
                ++namedCount;
        }

        if (namedCount >= Globals.numPlayers)
            return true;
        return false;
    }

}

