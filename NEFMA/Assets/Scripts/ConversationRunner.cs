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
    public UnityEngine.UI.Text speakerOut;   // Set directly in Unity.
    public UnityEngine.UI.Text textOut;      // Set directly in Unity.
    public UnityEngine.AudioSource voAudio;  // Set directly in Unity.
    public string convName;                  // Set a default conversation.

    protected Conversation conversation;
    protected int timeUntilUpdate;            // Tracks expected time until the next dialogue should load.
    protected int currIndex;                  // Tracks which dialogue in the conversation we are on.
    protected bool conversationLoaded;        // True if a conversation has been successfully loaded from disk.

    // Use this for initialization
    void Start () {
        conversationLoaded = false;
        currIndex = 0;

        if (convName.Length > 0) {
            startConversation(convName);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!conversationLoaded) { return; }

        timeUntilUpdate = timeUntilUpdate - 500;
        if (timeUntilUpdate <= 0) {
            next();
        }
	}

    public void startConversation(string conversationPath) {
        loadConversation(conversationPath);
        // Handle conversation load failure as gracefully as we can...
        if (conversationLoaded) {
            currIndex = 0;
            updateLine(0);
        }
    }

    // Move to next dialogue entry, or if moving past end of conversation, close dialogue
    public void next() {
        if (voAudio) { voAudio.Stop(); }

        if (conversation.isEOF(currIndex)) {
            this.end();
            return;
        }

        currIndex = currIndex + 1;
        updateLine(currIndex);
    }

    // Reset tracking vars, empty out output strings.
    public void end() {
        if (!conversationLoaded) { return; }

        this.clear();

        conversationLoaded = false;
        currIndex = 0;  
    }

    // Load a conversation from the provided file path.
    protected void loadConversation(string conversationPath)
    {
        // From Unity3D - Loading Game Data Json
        // Path.Combine combines strings into a file path.
        string filePath = Path.Combine(Application.streamingAssetsPath, conversationPath);
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            conversation = JsonUtility.FromJson<Conversation>(data);
            Debug.Log(conversation.dialogue.Count);
            conversationLoaded = true;
            currIndex = 0;
        }
        else
        {
            Debug.Log(Application.streamingAssetsPath);
            Debug.LogError("ERROR: Failed to load conversation!");
            conversationLoaded = false;
        }

    }

    protected void playVOLine(int index) {
        if (!voAudio) { return; }
        voAudio.Stop();
        
        // If a voice over line exists for this index {
        // TODO: Play this VO line now.
        // voAudio.PlayOneShot(...)

        // timeUntilUpdate = voAudio.clip.length;
        // }
    }

    // Update display variables for the new line and start VO.
    protected void updateLine(int index) {
        if (conversation == null) { return; }

        if (conversationLoaded) {
            //speakerOut.text = loadedConversation.getSpeaker(index);
            //textOut.text = loadedConversation.getText(index);
            timeUntilUpdate = conversation.getDisplayTime(index);
            playVOLine(index);

            Debug.Log(index);
            Debug.Log(conversation.getSpeaker(index) );
            Debug.Log(conversation.getText(index) );
            Debug.Log(conversation.getDisplayTime(index) );
        }
    }

    // Clears display variables to hide window.
    protected void clear() {
        //speakerOut.text = "";
        //textOut.text = "";
        timeUntilUpdate = 0;
    }

}
