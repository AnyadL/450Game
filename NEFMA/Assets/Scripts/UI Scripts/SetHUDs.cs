using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHUDs : MonoBehaviour {

    public Sprite agniPortrait;
    public Sprite rykerPortrait;
    public Sprite delilahPortrait;
    public Sprite kittyPortrait;

    public Sprite agniDead;
    public Sprite rykerDead;
    public Sprite delilahDead;
    public Sprite kittyDead;

    private Player player;
    private string newName;
    private string tempName = "";
    private string[] orderedNames = new string[4] { "Agni", "Ryker" , "Delilah", "Kitty"};

    private bool HUDactive = true;

    // Use this for initialization
    public void Start () {
        int numberOfHuds = Globals.players.Count;
        int i = 0;
        if (gameObject.transform.FindChild("DialoguePanel"))
        {
            // Don't do hud stuff
            HUDactive = false;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (Globals.players.Count > i)
                {
                    child.gameObject.SetActive(true);
                    setImage(i, child, true);
                }
                ++i;
            }
        }     
    }

    void Update()
    {
        int i = 0;
        if(Globals.players.Count < orderedNames.Length && HUDactive)
        {
            foreach (Transform child in transform)
            {
                if (Globals.players.Count > i)
                {
                    setImage(i, child);
                    if (!Globals.players[i].Alive)
                        testInput(Globals.players[i]);

                }
                ++i;
            }
        }
    }

    void testInput(Player player)
    {
        // left bumper
        if (Input.GetButtonDown("Fire1_" + player.InputNum))
        {
            print("left pressed");
            int i = getIndexOfName(player.Name);
            newName = getNewName(player.Name, "left", i);
            updatePlayer(player, newName);
        }
        // right bumper
        if (Input.GetButtonDown("Fire2_" + player.InputNum))
        {
            print("right pressed");
            int i = getIndexOfName(player.Name);
            newName = getNewName(player.Name, "right", i);
            updatePlayer(player, newName);
        }
    }

    int getIndexOfName(string name)
    {
        for (int i = 0; i < orderedNames.Length; ++i)
        {
            if (orderedNames[i] == name)
                return i;
        }
        return -1;
    }

    string getNewName(string name, string direction, int ind)
    {
        if (direction == "left")
        {
            ind = ind == 0 ? orderedNames.Length - 1 : ind - 1;
            tempName = orderedNames[ind];
            if (isNameInUse(tempName))
                getNewName(tempName, "left", ind);
        }
        else
        {
            ind = ind == orderedNames.Length - 1 ? 0 : ind + 1;
            tempName = orderedNames[ind];
            if (isNameInUse(tempName))
                getNewName(tempName, "right", ind);

        }
        return tempName;
    }

    bool isNameInUse(string name)
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Name == name)
                return true;
        }
        return false;
    }

    void updatePlayer(Player player, string updatedName)
    {
        Globals.players[player.Number].Name = updatedName;
        Globals.players[player.Number].Prefab = Resources.Load(newName) as GameObject;
    }

    void setImage(int i, Transform child, bool ignoreDeath = false)
    {
        player = Globals.players[i];
        if (player.Name == "Agni")
            child.GetChild(0).GetComponent<Image>().sprite = player.Alive || ignoreDeath ? agniPortrait : agniDead;
        else if (player.Name == "Ryker")
            child.GetChild(0).GetComponent<Image>().sprite = player.Alive || ignoreDeath ? rykerPortrait : rykerDead;
        else if (player.Name == "Delilah")
            child.GetChild(0).GetComponent<Image>().sprite = player.Alive || ignoreDeath ? delilahPortrait : delilahDead;
        else if (player.Name == "Kitty")
            child.GetChild(0).GetComponent<Image>().sprite = player.Alive || ignoreDeath ? kittyPortrait : kittyDead;
        if (player.Name != null && child.childCount > 3)
        {
            Globals.players[i].ScoreCounter = child.GetChild(3).GetComponent<UnityEngine.UI.Text>();
            Globals.players[i].ScoreCounter.text = Globals.players[i].Score.ToString();
        }
    }
}
