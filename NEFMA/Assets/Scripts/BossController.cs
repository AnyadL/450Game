using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [HideInInspector] public GameObject myHead;
    [HideInInspector] public GameObject myTail;
    [HideInInspector] public GameObject myLeftHand;
    [HideInInspector] public GameObject mySecretLeftHand;
    [HideInInspector] public GameObject myRightHand;
    [HideInInspector] public GameObject mySecretRightHand;
    [HideInInspector] public BossHead myHeadScript;
    [HideInInspector] public BossTail myTailScript;
    [HideInInspector] public BossHand myLeftHandScript;
    [HideInInspector] public BossHand myRightHandScript;
    [HideInInspector] public BossSecretHands mySecretLeftHandScript;
    [HideInInspector] public BossSecretHands mySecretRightHandScript;
    public GameObject allyPrefab1;
    public GameObject allyPrefab2;
    public GameObject allyPrefab3;
    [HideInInspector] public float nextFireball;
    public float fireballCooldown = 15f;
    [HideInInspector] public float nextSpawn;
    public float spawnCooldown = 15f;
    private int waveCount = 1;
    public Sprite angryHead;
    public Sprite neutralHead;
    public Sprite openHead;
    private int headChoice = 0;
    public GameObject bottomLeft;
    public GameObject bottomRight;
    public GameObject topLeft;
    public GameObject topRight;
    public AudioSource sfxRoar;
    public AudioSource sfxGrowl;

    // Use this for initialization
    void Start () {
        nextFireball = Time.time + fireballCooldown;
        nextSpawn = Time.time + spawnCooldown;
    }
	
	// Update is called once per frame
	void Update () {
        if (nextFireball <= Time.time)
        {
            StartCoroutine(fireballHead());
            myHeadScript.fireballs();
            nextFireball = Time.time + fireballCooldown;
        }
        if (nextSpawn <= Time.time)
        {
            spawnAllies();
            nextSpawn = Time.time + spawnCooldown;
        }
    }

    void spawnAllies()
    {
        if (waveCount % 5 == 0)
        {
            wave3();
        }
        else
        {
            wave1();
        }
        waveCount++;
    }

    // spawns loafers
    void wave1()
    {
        Instantiate(allyPrefab1, (transform.position + (transform.right * 72.5f) - (transform.up * 20)), Quaternion.identity);
        GameObject enemy2 = Instantiate(allyPrefab1, (transform.position - (transform.right * 72.5f) - (transform.up * 20)), Quaternion.identity);
        enemy2.GetComponent<EnemyAI>().Flip();
    }

    // spawns soakers
    void wave2(float xForce, float yForce)
    {
        GameObject enemy1 = Instantiate(allyPrefab2, (transform.position + (transform.right * 73f) - (transform.up * 20)), Quaternion.identity);
        GameObject enemy2 = Instantiate(allyPrefab2, (transform.position - (transform.right * 73f) - (transform.up * 20)), Quaternion.identity);
        enemy2.GetComponent<EnemyAI>().Flip();
        enemy1.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xForce, yForce));
        enemy2.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce));
    }

    // spawns ghosts
    void wave3()
    {
        if (waveCount % 10 == 0)
        {
            GameObject enemy = Instantiate(allyPrefab3, (transform.position - (transform.right * 65) + (transform.up * 50)), Quaternion.identity);
            enemy.GetComponent<EnemyAI>().Flip();
        }
        else
        {
            Instantiate(allyPrefab3, (transform.position + (transform.right * 65) + (transform.up * 50)), Quaternion.identity);
        }
    }

    // Boss has more than 75% health
    public void phaseOne()
    {
        //Debug.Log("------------------------------");
        //Debug.Log("Enter Phase 1");
        //printValues();
    }

    // Boss health between 50% - 75%
    public void phaseTwo()
    {
        //Debug.Log("------------------------------");
        //Debug.Log("Entering Phase 2");
        roar();
        Globals.currentCheckpoint.resPlayers();
        myTailScript.attacking = true;
        myLeftHandScript.waitTime = 0.833f;
        myRightHandScript.waitTime = 0.833f;
        myLeftHandScript.downForce += 1;
        myLeftHandScript.upForce += 0.1f;
        myRightHandScript.downForce += 1;
        myRightHandScript.upForce += 0.1f;
        spawnCooldown -= 2.5f;
        //printValues();
        wave2(800, 3500);
    }

    // Boss health between 25% - 50%
    public void phaseThree()
    {
        //Debug.Log("------------------------------");
        //Debug.Log("Entering Phase 3");
        roar();
        Globals.currentCheckpoint.resPlayers();
        myTailScript.numberOfNeedles += 3;
        myLeftHandScript.waitTime = 0.667f;
        myRightHandScript.waitTime = 0.667f;
        myLeftHandScript.downForce += 1;
        myLeftHandScript.upForce += 0.1f;
        myRightHandScript.downForce += 1;
        myRightHandScript.upForce += 0.1f;
        spawnCooldown -= 2.5f;
        //fireballCooldown += 3.14f;
        Debug.Log("Destroying Lower Clouds");
        DestroyObject(bottomLeft);
        DestroyObject(bottomRight);
        //printValues();
    }

    // Boss health between 0% - 25%
    public void phaseFour()
    {
        //Debug.Log("------------------------------");
        //Debug.Log("Entering Phase 4");
        roar();
        Globals.currentCheckpoint.resPlayers();
        myTailScript.numberOfNeedles += 3;
        myLeftHandScript.waitTime = 0.50f;
        myRightHandScript.waitTime = 0.50f;
        myLeftHandScript.downForce += 1;
        myLeftHandScript.upForce += 0.1f;
        myRightHandScript.downForce += 1;
        myRightHandScript.upForce += 0.1f;
        spawnCooldown -= 2.5f;
        //fireballCooldown += 3.14f;
        Debug.Log("Destroying Upper Clouds");
        DestroyObject(topLeft);
        DestroyObject(topRight);
        //printValues();
        wave2(1500, 1500);
    }

    public void printValues()
    {
        Debug.Log("numberOfNeedles: " + myTailScript.numberOfNeedles);
        Debug.Log("hand waitTime: " + myLeftHandScript.waitTime);
        Debug.Log("hand downForce: " + myLeftHandScript.downForce);
        Debug.Log("hand upForce: " + myLeftHandScript.upForce);
        Debug.Log("spawnCooldown: " + spawnCooldown);
        //Debug.Log("fireballCooldown: " + fireballCooldown);
    }

    // Justin put Roar Code here
    public void roar()
    {
        StartCoroutine(fireballHead());

        // ROAR
        if (sfxRoar != null && !sfxRoar.isPlaying)
        {
            sfxRoar.pitch = Random.Range(0.8f, 1.4f);
            sfxRoar.Play();
        }

        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].Alive)
            {
                Globals.players[i].GO.GetComponent<AttributeController>().knockback(transform.position.x, 40, 20);
            }
        }
    }

    public void headChange(int num) {
        if (headChoice == 2)
        {
            headChoice = num;
            return;
        }
        if (num == 0)
        {
            myHead.GetComponent<SpriteRenderer>().sprite = neutralHead;
            headChoice = 0;
        }
        else if (num == 1)
        {
            myHead.GetComponent<SpriteRenderer>().sprite = angryHead;
            headChoice = 1;
            if (sfxGrowl != null && !sfxGrowl.isPlaying)
            {
                sfxGrowl.pitch = Random.Range(0.8f, 1.4f);
                sfxGrowl.Play();
            }
        }
        else if (num == 2)
        {
            myHead.GetComponent<SpriteRenderer>().sprite = openHead;
            headChoice = 2;
            if (sfxRoar != null && !sfxRoar.isPlaying)
            {
                sfxRoar.pitch = Random.Range(0.8f, 1.4f);
                sfxRoar.Play();
            }

        }
    }

    IEnumerator fireballHead()
    {
        headChange(2);
        yield return new WaitForSeconds(2f);
        headChange(headChoice);
    }

    public void bossKill()
    {
        StartCoroutine(bossMove());
    }

    IEnumerator bossMove()
    {
        yield return new WaitForSeconds(1f);
        Globals.gamePaused = false;
        Globals.fading = true;
        Globals.fadeDir = 1;
    }

    // left = -1, right = 1
    public void registerHand(GameObject part, int num)
    {
        if (num == -1)
        {
            if (myLeftHand == null)
            {
                myLeftHand = part;
                myLeftHandScript = myLeftHand.GetComponent<BossHand>();
                if (mySecretLeftHandScript != null)
                {
                    mySecretLeftHandScript.linkHand(myLeftHand);
                }
            }
        }
        else if (num == 1)
        {
            if (myRightHand == null)
            {
                myRightHand = part;
                myRightHandScript = myRightHand.GetComponent<BossHand>();
                if (mySecretRightHandScript != null)
                {
                    mySecretRightHandScript.linkHand(myRightHand);
                }
            }
        }
    }

    // left = -1, right = 1
    public void registerSecretHand(GameObject part, int num)
    {
        if (num == -1)
        {
            if (mySecretLeftHand == null)
            {
                mySecretLeftHand = part;
                mySecretLeftHandScript = mySecretLeftHand.GetComponent<BossSecretHands>();
                if (myLeftHand != null)
                {
                    mySecretLeftHandScript.linkHand(myLeftHand);
                }
            }
        }
        else if (num == 1)
        {
            if (mySecretRightHand == null)
            {
                mySecretRightHand = part;
                mySecretRightHandScript = mySecretRightHand.GetComponent<BossSecretHands>();
                if (myRightHand != null)
                {
                    mySecretRightHandScript.linkHand(myRightHand);
                }
            }
        }
    }

    public void registerHead(GameObject part)
    {
        if (myHead == null)
        {
            myHead = part;
            myHeadScript = myHead.GetComponent<BossHead>();
        }
    }

    public void registerTail(GameObject part)
    {
        if (myTail == null)
        {
            myTail = part;
            myTailScript = myTail.GetComponent<BossTail>();
        }
    }
}
