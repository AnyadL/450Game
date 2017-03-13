using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayInstructions : MonoBehaviour {

    public Texture pressAtoStart;
    public Texture blankSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int countChosen = 0;
		for (var i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Name != "")
                ++countChosen;
        }
        if (countChosen == Globals.players.Count && Globals.players.Count != 0)
            gameObject.GetComponent<RawImage>().texture = pressAtoStart;
        else
            gameObject.GetComponent<RawImage>().texture = blankSprite;
    }
}
