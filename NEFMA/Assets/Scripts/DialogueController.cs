using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour {

    public GameObject questionPrefab;
    public GameObject exclamationPrefab;
    public GameObject ellipsisPrefab;

    private GameObject bubble;

    private GameObject playerObject;
    private Vector3 localOffset = new Vector3(-5, 20, 0);
    private Vector3 agniOffset = new Vector3(-5, 15, 0);

    //private List<GameObject> players = new List<GameObject>();
    private string[] orderedNames = new string[4] { "Agni", "Ryker", "Delilah", "Kitty" };

    private float pause = 1f;
    public List<string> conversation = new List<string>();
    private int currentLine = 0;
    private string currentWord;
    private int currentTime;
    private GameObject currentBubble;
    private string currentSpeaker;
    private float nextLine;

    private bool sceneOn = true;
    private GameObject kitty;

    
    // Use this for initialization
    void Start () {
	    for (int i = 0; i < orderedNames.Length; ++i)
        {
            playerObject = findPlayerObject(orderedNames[i]);
            createBubble(orderedNames[i], questionPrefab);
            createBubble(orderedNames[i], exclamationPrefab);
            createBubble(orderedNames[i] , ellipsisPrefab);
        }
        nextLine = Time.time + pause;
        playerObject = findPlayerObject("Kitty");
        kitty = playerObject;
        playerObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (sceneOn)
        {
            if (Time.time >= nextLine)
            {
                if (currentBubble)
                    currentBubble.SetActive(false);
                setLineVariables();
                playLine();
                ++currentLine;
            }
        }
    }

    void setLineVariables()
    {
        if (currentLine < conversation.Count)
        {
            setNextWord();
            if (currentWord == "Kitty")
                kitty.SetActive(true);
            playerObject = findPlayerObject(currentWord);
            setNextWord();
            currentBubble = playerObject.transform.FindChild(currentWord + "Bubble(Clone)").gameObject;
            currentBubble.SetActive(true);
            setNextWord();
            currentTime = System.Int32.Parse(currentWord);
            nextLine = Time.time + currentTime;
        }
        else
        {
            if (currentBubble)
                currentBubble.SetActive(false);
            Debug.Log("END SCENE.");
            sceneOn = false;
        }
    }

    void playLine()
    {   
        if (sceneOn)
            Debug.Log(conversation[currentLine]);
    }

    void setNextWord()
    {
        currentWord = conversation[currentLine].Substring(0, conversation[currentLine].IndexOf(' '));
        conversation[currentLine] = conversation[currentLine].Substring(conversation[currentLine].IndexOf(' ') + 1);
        //print(currentWord);
    }

    void createBubble(string name, GameObject prefab)
    {
        bubble = Instantiate(prefab) as GameObject;
        bubble.transform.parent = playerObject.transform;
        bubble.transform.localScale = new Vector3(5, 5, 1);
        if (name == "Agni")
            bubble.transform.localPosition = agniOffset;
        else
            bubble.transform.localPosition = localOffset;

        bubble.SetActive(false);
        
    }

    GameObject findPlayerObject(string name)
    {
        return GameObject.Find(name);
    }

}