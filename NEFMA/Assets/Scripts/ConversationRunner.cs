/******************************************************************************
 * Author: Michael Morris
 * Course: NEFMA
 * File: ConversationRunner.cs
 * 
 * Description: ConversationRunner actually runs a conversation, and tracks
 * the current position of the dialogue in the conversation (ie. node 8).
 * Voiceover progress needs to be tracked here as well.
 * 
 * ***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationRunner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void loadConversation(Conversation newConversation) {
        conversation = newConversation;
        currentIndex = 0;
    }

    void setPane(DialoguePane newWindow) {
        window = newWindow;
    }


    public Conversation conversation;   // FIXME: Public only for testing, must be private for release!
    public DialoguePane window;         // FIXME: Public only for testing, must be private for release!
    public int currentIndex;            // FIXME: Public only for testing, must be private for release!
    

}
