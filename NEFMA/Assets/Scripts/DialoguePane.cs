/******************************************************************************
 * Author: Michael Morris
 * Course: NEFMA
 * File: DialoguePane.cs
 * 
 * Description: DialoguePane which reads through data in Conversation instances.
 * Should fade in and out and only update when a Conversation is active.
 * 
 * Credits: 
 * Rough structure based on the one suggested by Tony Li here:
 * https://forum.unity3d.com/threads/dialogue-system-tutorial.315618/
 * 
 * ***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePane : MonoBehaviour {

    void open() {
        // TODO: Open dialogue pane here.
    }

    void loadText(string text) {
        dialogueText.text = text;
        // TODO: Set dirty flag to force redraw here.
    }

    public void close() {
        // TODO: Close dialogue pane here.
    }

    public UnityEngine.UI.Text dialogueText;
    // TODO: Timer variable before advancing to next text.
}
