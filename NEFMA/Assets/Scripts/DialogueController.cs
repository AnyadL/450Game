using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    public UnityEngine.UI.Text speakerOut;   // Set directly in Unity.
    public UnityEngine.UI.Text textOut;      // Set directly in Unity.
    public UnityEngine.AudioSource voAudio;  // Set directly in Unity.

    public GameObject questionPrefab;
    public GameObject exclamationPrefab;
    public GameObject ellipsisPrefab;

    public GameObject drG;

    private GameObject bubble;

    private GameObject playerObject;
    private Vector3 localOffset = new Vector3(-5, 20, 0);
    private Vector3 agniOffset = new Vector3(-5, 15, 0);

    private string[] orderedNames = new string[4] { "Agni", "Ryker", "Delilah", "Kitty" };


    private GameObject currentBubble;
    private string currentSpeaker;


    private bool kittyHidden = false;
    private bool rykerFlipped = false;
    private bool drGtime = false;

    private Vector3 tempTransform;

    // Use this for initialization
    public void initialize() {

	    for (int i = 0; i < orderedNames.Length; ++i)
        {
            playerObject = findPlayerObject(orderedNames[i]);

            createBubble(orderedNames[i], questionPrefab);
            createBubble(orderedNames[i], exclamationPrefab);
            createBubble(orderedNames[i] , ellipsisPrefab);

            playerObject.GetComponent<HeroMovement>().moveCharacter(1, 400);
        }
        kittyHidden = true;
    }

    void FixedUpdate()
    {
        if (drGtime)
        {
            drG.transform.position = new Vector3(drG.transform.position.x + 0.5f, drG.transform.position.y, drG.transform.position.z);
        }
    }

    public void setNode(Node current) {
        // End conversation.
        if (current == null) {
            endDialogue();
            return;
        }

        // Deactivate old speaker and bubbles.
        string lastSpeaker = currentSpeaker;
        deactivateSpeaker(lastSpeaker);

        // Activate the new speaker and bubbles.
        currentSpeaker = current.Speaker;
        
        activateSpeaker(currentSpeaker, current.type);

        // Update Textbox UI elements with new data.
        updateUI(currentSpeaker, current.Text);
    }

    public void clearUI() {
        //speakerOut.text = "";
        //textOut.text = "";
    }

    protected void deactivateSpeaker(string speaker) {
        GameObject speakerObj = findPlayerObject(speaker);
        if (speakerObj != null) {

            if (currentBubble) {
                currentBubble.SetActive(false);
            }
        }
    }

    protected void activateSpeaker(string speaker, string type) {


        if (rykerFlipped)
        {
            drGtime = true;
            drG.transform.position = new Vector3(drG.transform.position.x, 27, drG.transform.position.z);

            // flip ryker back
            findPlayerObject("Ryker").GetComponent<HeroMovement>().Flip();
            rykerFlipped = false;

            // stop kitty's slide
            findPlayerObject("Kitty").GetComponent<Rigidbody2D>().isKinematic = true;
            findPlayerObject("Kitty").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            findPlayerObject("Kitty").GetComponent<Rigidbody2D>().isKinematic = false;
            findPlayerObject("Kitty").GetComponent<HeroMovement>().moveCharacter(1, 400);

        }

        GameObject speakerObj = findPlayerObject(speaker);
        

        // Jump Kitty into the scene
        if (speaker == "Kitty" && kittyHidden)
        {
            kittyHidden = false;

            speakerObj.GetComponent<HeroMovement>().jumpCharacter(3500f, 3500f);

            findPlayerObject("Ryker").GetComponent<HeroMovement>().Flip();
            rykerFlipped = true;
        }

        if (speakerObj != null) {

            if (currentBubble)
                currentBubble.SetActive(false);
            currentBubble = speakerObj.transform.FindChild(type + "Bubble(Clone)").gameObject;
            
            currentBubble.SetActive(true);
        }
    }

    protected void endDialogue() {
        deactivateSpeaker(currentSpeaker);
        clearUI();
        
    }
    public void createBubble(string name, GameObject prefab)
    {
        bubble = Instantiate(prefab) as GameObject;
        bubble.transform.parent = playerObject.transform;
        bubble.transform.localScale = new Vector3(5, 5, 1);
        if (name == "Agni")                                     // Why only different for Agni? Answer: she has a big head
            bubble.transform.localPosition = agniOffset;
        else
            bubble.transform.localPosition = localOffset;

        bubble.SetActive(false);
    }

    public void updateUI(string speaker, string text)
    {
        speakerOut.text = speaker;
        textOut.text = text;
    }

    public float playVoiceOver(string voFile) {
        if (!voAudio) { return 0.0f; }
        voAudio.Stop();

        // TODO: Play this VO line now.
        // voAudio.PlayOneShot(...)
        return 5.0f;   // FIXME: Needs to return voAudio.clip.length instead.
    }

    public void stopVoiceOver() {
        if (!voAudio) { return; }
        voAudio.Stop();
    }

    protected GameObject findPlayerObject(string name) {
        return GameObject.Find(name);
    }

}