using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHUDs : MonoBehaviour {

    public Sprite agniPortrait;
    public Sprite rykerPortrait;
    public Sprite delilahPortrait;
    public Sprite kittyPortrait;

    private Player player;

    // Use this for initialization
    void Start () {
        int numberOfHuds = Globals.players.Count;
        int i = 0;
        foreach (Transform child in transform)
        {
            if (Globals.players.Count > i)
            {
                child.gameObject.SetActive(true);
                setImage(i, child);
            }
            ++i;
        }
    }

    void setImage(int i, Transform child)
    {
        player = Globals.players[i];
        if (player.Name == "Agni")
            child.GetChild(0).GetComponent<Image>().sprite = agniPortrait;
        else if (player.Name == "Ryker")
            child.GetChild(0).GetComponent<Image>().sprite = rykerPortrait;
        else if (player.Name == "Delilah")
            child.GetChild(0).GetComponent<Image>().sprite = delilahPortrait;
        else if (player.Name == "Kitty")
            child.GetChild(0).GetComponent<Image>().sprite = kittyPortrait;
    }
}
