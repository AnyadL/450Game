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

    private GameObject bubble;
    private GameObject question;
    private GameObject exclamation;
    private GameObject ellipsis;

    private GameObject playerObject;
    private Vector3 localOffset = new Vector3(-5, 20, 0);
    private Vector3 agniOffset = new Vector3(-5, 15, 0);

    private List<GameObject> players = new List<GameObject>();
    private string[] orderedNames = new string[4] { "Agni", "Ryker", "Delilah", "Kitty" };

    //private float pause = 1f;
    //public List<string> conversation = new List<string>();
    //private int currentLine = 0;
    //private string currentWord;
    //private int currentTime;
    private GameObject currentBubble;
    private string currentSpeaker;
    //private float nextLine;

    //private bool sceneOn = true;
    private GameObject kitty;

    
    // Use this for initialization
    public void initialize() {
	    for (int i = 0; i < orderedNames.Length; ++i)
        {
            playerObject = findPlayerObject(orderedNames[i]);
            question = createBubble(orderedNames[i], questionPrefab);
            exclamation = createBubble(orderedNames[i], exclamationPrefab);
            ellipsis = createBubble(orderedNames[i] , ellipsisPrefab);
        }
        //nextLine = Time.time + pause;       // Timing handled in ConversationRunner.cs.
        playerObject = findPlayerObject("Kitty");   // Why is this different from the others? - because Kitty only shows up partway through!
        kitty = playerObject;
        playerObject.SetActive(false);
    }
	
//	// Update is called once per frame
//	void Update () {
//        if (sceneOn)
//        {
//            if (Time.time >= nextLine)
//            {
//                if (currentBubble)
//                    currentBubble.SetActive(false);
//                setLineVariables();
//                playLine();
//                ++currentLine;
//            }
//        }
//    }

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
        activateSpeaker(currentSpeaker);

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
            //speakerObj.SetActive(false);
            // TODO: Set any bubbles related to speaker inactive here.
            if (currentBubble) {
                currentBubble.SetActive(false);
            }
        }
    }

    protected void activateSpeaker(string speaker) {
        GameObject speakerObj = findPlayerObject(speaker);
        if (speakerObj != null) {
            speakerObj.SetActive(true);
            // FIXME: What is significance of currentWord here?  Can this be passed in from JSON as well?
            //currentBubble = playerObject.transform.FindChild(currentWord + "Bubble(Clone)").gameObject;
            //currentBubble.SetActive(true);
        }
    }

    protected void endDialogue() {
        deactivateSpeaker(currentSpeaker);
        clearUI();

        Debug.Log("END SCENE.");
//      sceneOn = false;
    }

//    void setLineVariables()
//    {
//        if (currentLine < conversation.Count)
//        {
//            setNextWord();
//            if (currentWord == "Kitty")
//                kitty.SetActive(true);
//            playerObject = findPlayerObject(currentWord);
//            setNextWord();
//            currentBubble = playerObject.transform.FindChild(currentWord + "Bubble(Clone)").gameObject;
//            currentBubble.SetActive(true);
//            setNextWord();
//            currentTime = System.Int32.Parse(currentWord);  // ????
//            nextLine = Time.time + currentTime;
//       }
//     }
//
//    void playLine()
//    {
//        Debug.Log(conversation[currentLine]);
//    }
//
//    void setNextWord()
//    {
//        currentWord = conversation[currentLine].Substring(0, conversation[currentLine].IndexOf(' '));
//        conversation[currentLine] = conversation[currentLine].Substring(conversation[currentLine].IndexOf(' ') + 1);
//        print(currentWord);
//    }

    protected GameObject createBubble(string name, GameObject prefab) {
        bubble = Instantiate(prefab) as GameObject;
        bubble.transform.parent = playerObject.transform;
        bubble.transform.localScale = new Vector3(5, 5, 1);
        if (name == "Agni")                                     // TODO: Why only different for Agni?
            bubble.transform.localPosition = agniOffset;
        else
            bubble.transform.localPosition = localOffset;

        bubble.SetActive(false);
        return bubble;
    }

    public void updateUI(string speaker, string text)
    {
        //speakerOut.text = speaker;
        //textOut.text = text;
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