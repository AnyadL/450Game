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

    public Sprite sittingMilo;
    public Sprite pointingMilo;
    public Sprite visibleMilo;


    public GameObject mrFluffy;

    public GameObject[] monsterList;

    public Texture2D fadeTexture;

    private GameObject bubble;

    private GameObject playerObject;
    private Vector3 localOffset = new Vector3(-5, 20, 0);
    private Vector3 agniOffset = new Vector3(-5, 15, 0);

    private string[] orderedNames = new string[6] { "Agni", "Ryker", "Delilah", "Kitty", "Milo", "Dr. G" };


    private GameObject currentBubble;
    private string currentSpeaker;

    private int currentSpecial = 0;
    private float timePause = 0f;
    private float updateTime = 0f;
    private string convoName;
    
    private bool drGtime = false;
    private float drGmove = 0f;
    private bool monsterTime = false;
    private bool bossTime = false;
    private bool handsTime = false;
    private bool headTime = false;
    private bool handsUp = true;
    private bool endTime = false;
    //private bool monstersMoving = false;
    private bool miloMoved = false;
    private float[] schreiHeights = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    
    private Vector3 tempTransform;


    public AudioSource sfxRoar;

    // Use this for initialization
    public void initialize(string conversationName) {

        convoName = conversationName;

	    for (int i = 0; i < orderedNames.Length; ++i)
        {
            playerObject = findPlayerObject(orderedNames[i]);
            if (playerObject != null)
            {
                // create bubbles for each hero
                createBubble(orderedNames[i], questionPrefab);
                createBubble(orderedNames[i], exclamationPrefab);
                createBubble(orderedNames[i], ellipsisPrefab);

            }
        }

        int j = 0;
        if (monsterList != null)
        {
            for (int i = 0; i < monsterList.Length; ++i)
            {
                if (monsterList[i].name == "Schrei")
                {
                    schreiHeights[j] = monsterList[i].transform.position.y;
                    ++j;
                }

            }
        }
        
    }

    void FixedUpdate()
    {
        if (convoName == "introConversation.json")
        {
            doIntro();
        }
        else if (convoName == "preBoss.json")
        {
            doPreBoss();
        }
        else if (convoName == "noExcuse.json")
        {
            doNoExcuse();
        }
        else if (convoName == "monsterAbuse.json")
        {
            doMonsterAbuse();
        }
        else if (convoName == "noMonsterAbuse.json")
        {
            doNoMonsterAbuse();
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

    void doIntro()
    {
        switch (currentSpecial)
        {
            case 0:
                break;
            case 1:
                // start walking
                currentSpecial = 0;
                for (int i = 0; i < orderedNames.Length; ++i)
                {
                    playerObject = findPlayerObject(orderedNames[i]);
                    if (playerObject != null && playerObject.name != "Dr. G")
                        playerObject.GetComponent<HeroMovement>().moveCharacter(1, 400);
                }
                break;
            case 2:
                if (timePause == 0)
                {
                    // Jump Kitty onto screen

                    timePause = 1.4f;// stop Kitty jump in 1 second

                    updateTime = Time.time + timePause;

                    findPlayerObject("Kitty").GetComponent<HeroMovement>().jumpCharacter(3000f, 3300f);
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
                    if (playerObject != null && playerObject.name != "Dr. G")
                        playerObject.GetComponent<HeroMovement>().moveCharacter(1, 700);
                }
                break;
            case 7:
                if (timePause == 0)
                {
                    timePause = 3f;

                    updateTime = Time.time + timePause;
                }
                else if (Time.time >= updateTime)
                {
                    // fade out
                    Globals.gamePaused = false;
                    Globals.fading = true;
                    Globals.fadeDir = 1;
                }
                break;
        }
        if (drGtime)
            drG.transform.position = new Vector3(drG.transform.position.x + 0.5f, 25f + (Mathf.Sin(2 * Time.time) * 5), drG.transform.position.z);
    }

    void doPreBoss()
    {
        switch (currentSpecial)
        {
            case 0:
                break;
            case 1:

                currentSpecial = 0;
                drGtime = true;
                break;
            case 2:
                currentSpecial = 0;
                findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = visibleMilo;
                break;
            case 3:
                currentSpecial = 0;
                findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = pointingMilo;
                break;
            case 4:
                currentSpecial = 0;
                findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = visibleMilo;
                break;
            case 5:
                if (timePause == 0)
                {
                    //timePause = 15f;
                    timePause = 1f;

                    updateTime = Time.time + timePause;
                }
                else if (Time.time >= updateTime)
                {
                    timePause = 1f;

                    updateTime = Time.time + timePause;
                    currentSpecial = 0;

                    endTime = true;
                    monsterTime = true;
                    handsTime = true;
                    drGmove = 0.05f;
                }
                break;
        }
        if (drGtime)
            drG.transform.position = new Vector3(drG.transform.position.x + drGmove, 12f + (Mathf.Sin(2 * Time.time) * 2), drG.transform.position.z);
        if (endTime)
        {
            if(monsterTime)
            {
                if (handsTime)
                {
                    GameObject bosshandright = findPlayerObject("BossHandRight");
                    GameObject bosshandleft = findPlayerObject("BossHandLeft");
                    print("bosshandleft y:" + bosshandleft.transform.position.y);
                    if (handsUp)
                    {
                        bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y + 1f, bosshandright.transform.position.z);
                        bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y + 1f, bosshandleft.transform.position.z);
                    }
                    else
                    {
                        bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y - 1f, -1);
                        bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y - 1f, -1);
                    }
                    
                    if (bosshandright.transform.position.y >= -6 && handsUp)
                    {
                        handsUp = false;
                    }
                    else if (bosshandright.transform.position.y < -9 && !handsUp)
                    {
                        headTime = true;
                        handsTime = false;
                    }
                }
                if (headTime)
                {
                    GameObject bosshead = findPlayerObject("BossHead");
                    GameObject bosshandright = findPlayerObject("BossHandRight");
                    GameObject bosshandleft = findPlayerObject("BossHandLeft");

                    bosshead.transform.position = new Vector3(bosshead.transform.position.x, bosshead.transform.position.y + 0.4f, bosshead.transform.position.z);

                    bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y - 0.4f, -1);
                    bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y - 0.4f, -1);
                    if (bosshead.transform.position.y >= 13f)
                        monsterTime = false;
                }

            }

            if (Time.time >= updateTime)
            {
                if (!miloMoved)
                {
                    timePause = 0;
                    miloMoved = true;
                    findPlayerObject("Milo").GetComponent<HeroMovement>().jumpCharacter(-2050f, 3300f);
                    findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = sittingMilo;
                    Vector2 milosize = findPlayerObject("Milo").GetComponent<BoxCollider2D>().size;
                    findPlayerObject("Milo").GetComponent<BoxCollider2D>().size = new Vector2(milosize.x, 17f);
                    findPlayerObject("Main Camera").GetComponent<Camera2DFollow>().unLinkPlayers(findPlayerObject("BossHead").transform);
                    timePause = 2f;
                    updateTime = Time.time + timePause;
                }
                else
                {
                    // fade out
                    Globals.gamePaused = false;
                    Globals.fading = true;
                    Globals.fadeDir = 1;
                }

            }
            
        }
            
    }

    void doNoExcuse ()
    {

        switch (currentSpecial)
        {
            case 0:
                break;
            case 1:
                if (timePause == 0)
                {

                    findPlayerObject("Milo").GetComponent<HeroMovement>().jumpCharacter(2000f, 1000f);
                    findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = visibleMilo;
                    drGmove = -0.05f;
                    timePause = 1f;
                    updateTime = Time.time + timePause;
                    drGtime = true;
                    monsterTime = true;
                    headTime = true;
                    handsTime = false;
                    handsUp = true;

                    // ROAR
                    if (sfxRoar != null && !sfxRoar.isPlaying)
                    {
                        sfxRoar.pitch = Random.Range(0.8f, 1.4f);
                        sfxRoar.Play();
                    }

                }
                else if (Time.time >= updateTime)
                {
                    currentSpecial = 0;
                    timePause = 0;
                    findPlayerObject("Milo").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                }
                break;

            case 2:
                if (timePause == 0)
                {
                    timePause = 1.5f;

                    updateTime = Time.time + timePause;
                }
                else if (timePause == 1.5f && Time.time >= updateTime)
                {
                    timePause = 1.9f;

                    updateTime = Time.time + timePause;

                    mrFluffy.GetComponent<HeroMovement>().moveCharacter(1, 2450);
                }
                else if (Time.time >= updateTime)
                {
                    mrFluffy.GetComponent<Rigidbody2D>().isKinematic = true;
                    mrFluffy.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                    mrFluffy.GetComponent<Rigidbody2D>().isKinematic = false;


                    currentSpecial = 0;
                    timePause = 0;
                }

                break;
            case 3:
                if (timePause == 0)
                {
                    timePause = 3f;

                    updateTime = Time.time + timePause;
                }
                else if (timePause == 3f && Time.time >= updateTime)
                {
                    drGmove = -0.3f;
                    findPlayerObject("Milo").transform.FindChild("EllipsisBubble(Clone)").gameObject.SetActive(false);
                    mrFluffy.GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Milo").GetComponent<HeroMovement>().moveCharacter(-1, 600f);

                    timePause = 5.5f;

                    updateTime = Time.time + timePause;
                    
                }
                else if (timePause == 5.5f && Time.time >= updateTime)
                {
                    
                    findPlayerObject("Ryker").GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Agni").GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Kitty").GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Delilah").GetComponent<HeroMovement>().moveCharacter(-1, 600f);

                    timePause = 4f;

                    updateTime = Time.time + timePause;
                }
                else if (Time.time >= updateTime)
                {
                    // fade out
                    Globals.gamePaused = false;
                    Globals.fading = true;
                    Globals.fadeDir = 1;
                }
                break;
        }
        if (drGtime)
        {
            if (drG.transform.position.x <= 45f && drGmove != -0.3f)
                drGmove = 0;
            drG.transform.position = new Vector3(drG.transform.position.x + drGmove, 12f + (Mathf.Sin(2 * Time.time) * 2), drG.transform.position.z);
        }

        if (monsterTime)
        {
            if (headTime)
            {
                GameObject bosshead = findPlayerObject("BossHead");
                GameObject bosshandright = findPlayerObject("BossHandRight");
                GameObject bosshandleft = findPlayerObject("BossHandLeft");

                bosshead.transform.position = new Vector3(bosshead.transform.position.x, bosshead.transform.position.y - 0.7f, bosshead.transform.position.z);

                bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y + 0.7f, -1);
                bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y + 0.7f, -1);
                if (bosshead.transform.position.y <= -31f)
                {
                    headTime = false;
                    handsTime = true;
                }
            }
            if (handsTime)
            {
                GameObject bosshandright = findPlayerObject("BossHandRight");
                GameObject bosshandleft = findPlayerObject("BossHandLeft");

                if (handsUp)
                {
                    bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y + 1f, -1);
                    bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y + 1f, -1);
                }
                else
                {
                    bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y - 1f, 1);
                    bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y - 1f, 1);
                }

                if (bosshandright.transform.position.y >= -6 && handsUp)
                {
                    handsUp = false;
                }
                else if (bosshandright.transform.position.y < -20 && !handsUp)
                {
                    handsTime = false;
                    monsterTime = false;
                }
            }

        }
    }

    void doMonsterAbuse ()
    {
        switch (currentSpecial)
        {
            case 0:
                break;
            case 1:
                if (timePause == 0)
                {

                    findPlayerObject("Milo").GetComponent<HeroMovement>().jumpCharacter(2000f, 1000f);
                    findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = visibleMilo;
                    drGmove = -0.05f;
                    timePause = 1f;
                    updateTime = Time.time + timePause;
                    drGtime = true;
                    monsterTime = true;
                    headTime = true;
                    handsTime = false;
                    handsUp = true;

                    // ROAR
                    if (sfxRoar != null && !sfxRoar.isPlaying)
                    {
                        sfxRoar.pitch = Random.Range(0.8f, 1.4f);
                        sfxRoar.Play();
                    }

                }
                else if (Time.time >= updateTime)
                {
                    currentSpecial = 0;
                    timePause = 0;
                    findPlayerObject("Milo").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                }
                break;
            case 2:
                currentSpecial = 0;
                findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = pointingMilo;
                break;
            case 3:
                currentSpecial = 0;
                findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = visibleMilo;
                break;
            case 4:
                if (timePause == 0)
                {
                    timePause = 2f;

                    updateTime = Time.time + timePause;
                }
                else if (timePause == 2f && Time.time >= updateTime)
                {
                    timePause = 0.4f;

                    updateTime = Time.time + timePause;

                    findPlayerObject("Ryker").GetComponent<HeroMovement>().moveCharacter(1, 10600);
                    findPlayerObject("Ryker").GetComponent<Animator>().SetBool("AttackLittle", true);
                }
                else if (Time.time >= updateTime)
                {
                    
                    //findPlayerObject("Ryker").GetComponent<Rigidbody2D>().isKinematic = true;
                    findPlayerObject("Ryker").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                    findPlayerObject("Ryker").GetComponent<Animator>().SetBool("Walk", false);
                    findPlayerObject("Ryker").GetComponent<Animator>().SetBool("AttackLittle", false);
                    //findPlayerObject("Ryker").GetComponent<Rigidbody2D>().isKinematic = false;

                    currentSpecial = 0;
                    timePause = 0;
                }

                break;
            case 5:
                if (timePause == 0)
                {
                    timePause = 5f;

                    updateTime = Time.time + timePause;
                }
                else if (Time.time >= updateTime)
                {
                    // fade out
                    Globals.gamePaused = false;
                    Globals.fading = true;
                    Globals.fadeDir = 1;
                }
                break;
        }
        if (drGtime)
        {
            if (drG.transform.position.x <= 45f)
                drGmove = 0;
            drG.transform.position = new Vector3(drG.transform.position.x + drGmove, 12f + (Mathf.Sin(2 * Time.time) * 2), drG.transform.position.z);
        }
        if (monsterTime)
        {
            if (headTime)
            {
                GameObject bosshead = findPlayerObject("BossHead");
                GameObject bosshandright = findPlayerObject("BossHandRight");
                GameObject bosshandleft = findPlayerObject("BossHandLeft");

                bosshead.transform.position = new Vector3(bosshead.transform.position.x, bosshead.transform.position.y - 0.7f, bosshead.transform.position.z);

                bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y + 0.7f, -1);
                bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y + 0.7f, -1);
                if (bosshead.transform.position.y <= -31f)
                {
                    headTime = false;
                    handsTime = true;
                }
            }
            if (handsTime)
            {
                GameObject bosshandright = findPlayerObject("BossHandRight");
                GameObject bosshandleft = findPlayerObject("BossHandLeft");
                
                if (handsUp)
                {
                    bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y + 1f, -1);
                    bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y + 1f, -1);
                }
                else
                {
                    bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y - 1f, 1);
                    bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y - 1f, 1);
                }

                if (bosshandright.transform.position.y >= -6 && handsUp)
                {
                    handsUp = false;
                }
                else if (bosshandright.transform.position.y < -20 && !handsUp)
                {
                    handsTime = false;
                    monsterTime = false;
                }
            }

        }

    }

    void doNoMonsterAbuse ()
    {

        switch (currentSpecial)
        {
            case 0:
                break;
            case 1:
                if (timePause == 0)
                {

                    findPlayerObject("Milo").GetComponent<HeroMovement>().jumpCharacter(2000f, 1000f);
                    findPlayerObject("Milo").GetComponent<SpriteRenderer>().sprite = visibleMilo;
                    drGmove = -0.05f;
                    timePause = 1f;
                    updateTime = Time.time + timePause;
                    drGtime = true;
                    bossTime = true;
                    headTime = true;
                    handsTime = false;
                    handsUp = true;

                    // ROAR
                    if (sfxRoar != null && !sfxRoar.isPlaying)
                    {
                        sfxRoar.pitch = Random.Range(0.8f, 1.4f);
                        sfxRoar.Play();
                    }

                }
                else if (Time.time >= updateTime)
                {
                    currentSpecial = 0;
                    timePause = 0;
                    findPlayerObject("Milo").GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                }
                break;

            case 2:
                if (timePause == 0)
                {
                    timePause = 1.5f;

                    updateTime = Time.time + timePause;
                }
                else if (timePause == 1.5f && Time.time >= updateTime)
                {
                    timePause = 1.9f;

                    updateTime = Time.time + timePause;

                    mrFluffy.GetComponent<HeroMovement>().moveCharacter(1, 2450);
                }
                else if (Time.time >= updateTime)
                {
                    mrFluffy.GetComponent<Rigidbody2D>().isKinematic = true;
                    mrFluffy.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                    mrFluffy.GetComponent<Rigidbody2D>().isKinematic = false;


                    currentSpecial = 0;
                    timePause = 0;
                }

                break;
            case 3:
                if (timePause == 0)
                {
                    timePause = 3f;

                    updateTime = Time.time + timePause;
                }
                else if (timePause == 3f && Time.time >= updateTime)
                {
                    drGmove = -0.3f;
                    findPlayerObject("Milo").transform.FindChild("ExclamationBubble(Clone)").gameObject.SetActive(false);
                    mrFluffy.GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Milo").GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Milo").GetComponent<HeroMovement>().Flip();

                    timePause = 0;
                    currentSpecial = 0;
                }
                break;
            case 4:
                if (timePause == 0)
                {
                    
                    timePause = 2.5f;

                    updateTime = Time.time + timePause;
                }
                else if (timePause == 2.5f && Time.time >= updateTime)
                {

                    findPlayerObject("Ryker").GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Agni").GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Kitty").GetComponent<HeroMovement>().moveCharacter(-1, 600f);
                    findPlayerObject("Delilah").GetComponent<HeroMovement>().moveCharacter(-1, 600f);

                    monsterTime = true;
                    //monstersMoving = false;
                    timePause = 12f;
                    updateTime = Time.time + timePause;
                }
                else if (timePause == 12f && Time.time >= updateTime)
                {
                    // fade out
                    Globals.gamePaused = false;
                    Globals.fading = true;
                    Globals.fadeDir = 1;
                }
                break;
        }
        if (drGtime)
        {
            if (drG.transform.position.x <= 45f && drGmove != -0.3f)
                drGmove = 0;
            drG.transform.position = new Vector3(drG.transform.position.x + drGmove, 12f + (Mathf.Sin(2 * Time.time) * 2), drG.transform.position.z);
        }
        if (bossTime)
        {
            if (headTime)
            {
                GameObject bosshead = findPlayerObject("BossHead");
                GameObject bosshandright = findPlayerObject("BossHandRight");
                GameObject bosshandleft = findPlayerObject("BossHandLeft");

                bosshead.transform.position = new Vector3(bosshead.transform.position.x, bosshead.transform.position.y - 0.7f, bosshead.transform.position.z);

                bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y + 0.7f, -1);
                bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y + 0.7f, -1);
                if (bosshead.transform.position.y <= -31f)
                {
                    headTime = false;
                    handsTime = true;
                }
            }
            if (handsTime)
            {
                GameObject bosshandright = findPlayerObject("BossHandRight");
                GameObject bosshandleft = findPlayerObject("BossHandLeft");

                if (handsUp)
                {
                    bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y + 1f, -1);
                    bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y + 1f, -1);
                }
                else
                {
                    bosshandright.transform.position = new Vector3(bosshandright.transform.position.x, bosshandright.transform.position.y - 1f, 1);
                    bosshandleft.transform.position = new Vector3(bosshandleft.transform.position.x, bosshandleft.transform.position.y - 1f, 1);
                }

                if (bosshandright.transform.position.y >= -6 && handsUp)
                {
                    handsUp = false;
                }
                else if (bosshandright.transform.position.y < -20 && !handsUp)
                {
                    handsTime = false;
                    monsterTime = false;
                }
            }

        }
        if (monsterTime)
        {
            int j = 0;
            for (int i = 0; i < monsterList.Length; ++i)
            {
                if (monsterList[i].name == "Loafer" && monsterList[i].GetComponent<Rigidbody2D>().velocity.x == 0f)
                {
                    monsterList[i].GetComponent<HeroMovement>().moveCharacter(-1, 800f);
                    //monstersMoving = true;
                }
                else if (monsterList[i].name == "Soaker" && monsterList[i].transform.position.y < -9f)
                {
                    monsterList[i].GetComponent<HeroMovement>().jumpCharacter(Random.Range(-30f, -50f), 100f);
                }
                else if (monsterList[i].name == "Schrei")
                {
                    monsterList[i].transform.position = new Vector3(monsterList[i].transform.position.x - 0.3f, schreiHeights[j] + (Mathf.Sin(Random.Range(1,2) * Time.time) * 2), monsterList[i].transform.position.z);
                    ++j;
                }
                
            }
        }
    }
}