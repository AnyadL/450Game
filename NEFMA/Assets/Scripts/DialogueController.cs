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

    private int currentSpecial = 0;
    private float timePause = 0f;
    private float updateTime = 0f;
    private string convoName;
    
    private bool drGtime = false;

    private Vector3 tempTransform;

    // Use this for initialization
    public void initialize(string conversationName) {

        convoName = conversationName;

	    for (int i = 0; i < orderedNames.Length; ++i)
        {
            playerObject = findPlayerObject(orderedNames[i]);
            // create bubbles for each hero
            createBubble(orderedNames[i], questionPrefab);
            createBubble(orderedNames[i], exclamationPrefab);
            createBubble(orderedNames[i] , ellipsisPrefab);
        }
    }

    void FixedUpdate()
    {
        if (convoName == "introConversation.json")
        {
            switch(currentSpecial)
            {
                case 0:
                    break;
                case 1:
                    // start walking
                    currentSpecial = 0;
                    for (int i = 0; i < orderedNames.Length; ++i)
                    {
                        playerObject = findPlayerObject(orderedNames[i]);
                        playerObject.GetComponent<HeroMovement>().moveCharacter(1, 400);
                    }
                    break;
                case 2:
                    if (timePause == 0)
                    {
                        // Jump Kitty onto screen

                        timePause = 1f;// stop Kitty jump in 1 second

                        updateTime = Time.time + timePause;

                        findPlayerObject("Kitty").GetComponent<HeroMovement>().jumpCharacter(3500f, 3500f);
                    }
                    else if (Time.time >= updateTime)
                    {
                        
                        // stop kitty's slide
                        findPlayerObject("Kitty").GetComponent<Rigidbody2D>().isKinematic = true;
                        findPlayerObject("Kitty").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                        findPlayerObject("Kitty").GetComponent<Rigidbody2D>().isKinematic = false;
                        findPlayerObject("Kitty").GetComponent<HeroMovement>().moveCharacter(1, 400);

                        // Flip Ryker
                        findPlayerObject("Ryker").GetComponent<HeroMovement>().Flip();
                        findPlayerObject("Ryker").GetComponent<HeroMovement>().jumpCharacter(0, 1000f);

                        currentSpecial = 0;
                        timePause = 0;
                    }
                    break;
                case 3:
                    // Flip Ryker back

                    findPlayerObject("Ryker").GetComponent<HeroMovement>().Flip();
                    currentSpecial = 0;
                    break;
                case 4:
                    if (timePause == 0)
                    {
                        // Wait a bit then flip Agni

                        timePause = 3f;

                        updateTime = Time.time + timePause;
                    }
                    else if (Time.time >= updateTime)
                    {
                        findPlayerObject("Agni").GetComponent<HeroMovement>().Flip();

                        currentSpecial = 0;
                        timePause = 0;
                    }
                    break;
                case 5:
                    // Start moving Dr G
                    findPlayerObject("Agni").GetComponent<HeroMovement>().Flip();
                    drGtime = true;
                    currentSpecial = 0;
                    timePause = 0;
                    break;
                case 6:
                    // faster heroes
                    for (int i = 0; i < orderedNames.Length; ++i)
                    {
                        playerObject = findPlayerObject(orderedNames[i]);
                        // considered making Ryker faster, but he's a chicken
                        playerObject.GetComponent<HeroMovement>().moveCharacter(1, 700);
                    }
                    break;
            }
            if (drGtime)
                drG.transform.position = new Vector3(drG.transform.position.x + 0.5f, 25f + (Mathf.Sin(2*Time.time)* 5), drG.transform.position.z);
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
        currentSpecial = current.special;

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

        GameObject speakerObj = findPlayerObject(speaker);

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