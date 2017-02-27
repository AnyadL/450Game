/******************************************************************************
 * Author: Michael Morris
 * Course: NEFMA
 * File: Conversation.cs
 * 
 * Description: Handles storage and processing of dialogue nodes.  Handling is
 * not provided for seperate responses per node (it is not needed).  Will need
 * additional handling for voice over.
 * 
 * Credits: 
 * Rough structure based on the one suggested by Tony Li here:
 * https://forum.unity3d.com/threads/dialogue-system-tutorial.315618/
 * 
 * ***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Conversation : MonoBehaviour
{

    public bool isEOF(int index) {
        if (index >= dialogue.size) {
            return true;
        }

        return false;
    }

    public List<Node> dialogue; // TODO: Needs to become private.
}


[System.Serializable]
public class Node
{
    public string speaker;
    public string text;

    // TODO: Needs voiceover variables/handling
}