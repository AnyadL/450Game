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
using System.IO;

public class ConversationRunner : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    void loadConversation(string conversationPath) {
        // From Unity3D - Loading Game Data Json
        // Path.Combine combines strings into a file path.
        string filePath = Path.Combine(Application.streamingAssetsPath, conversationPath);
        if( File.Exists(filePath) ) {
            // Read the json from the file into a string
            string data = File.ReadAllText(filePath);

            // Pass the json to JsonUtility, and tell it to create a GameData object from it.
            Conversation loadedConversation = JsonUtility.FromJson<Conversation>(data);
            currentIndex = 0;
        }
        else {
            Debug.LogError("ERROR: Failed to load conversation!");
        }

    }

    void setPane(DialoguePane newWindow) {
        window = newWindow;
    }

    void advance() {
        int nextIndex = currentIndex + 1;
        if (conversation.isEOF(nextIndex)) {
            end();
            return;
        }

        currentIndex = nextIndex;
        // FIXME: Halt playing VO now.
        // FIXME: Start next VO line now.
    }

    void end() {
        window.close();
        
        conversation = null;
        currentIndex = 0;
    }

    public Conversation conversation;   // FIXME: Public only for testing, must be private for release!
    public DialoguePane window;         // FIXME: Public only for testing, must be private for release!
    public int currentIndex;            // FIXME: Public only for testing, must be private for release!
    

}