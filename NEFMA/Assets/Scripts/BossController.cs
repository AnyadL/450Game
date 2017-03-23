using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [HideInInspector] public GameObject myHead;
    [HideInInspector] public GameObject myLeftHand;
    [HideInInspector] public GameObject mySecretLeftHand;
    [HideInInspector] public GameObject myRightHand;
    [HideInInspector] public GameObject mySecretRightHand;
    [HideInInspector] public BossHead myHeadScript;
    [HideInInspector] public BossHand myLeftHandScript;
    [HideInInspector] public BossHand myRightHandScript;
    [HideInInspector] public BossSecretHands mySecretLeftHandScript;
    [HideInInspector] public BossSecretHands mySecretRightHandScript;
    public GameObject allyPrefab;
    [HideInInspector] public float nextFireball;
    public float fireballCooldown = 15f;
    [HideInInspector] public float nextSpawn;
    public float spawnCooldown = 15f;

    // Use this for initialization
    void Start () {
        nextFireball = Time.time + fireballCooldown;
        nextSpawn = Time.time + spawnCooldown;
    }
	
	// Update is called once per frame
	void Update () {
        if (nextFireball <= Time.time)
        {
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
        Instantiate(allyPrefab, (transform.position + (transform.right * 72.5f) - (transform.up * 16)), Quaternion.identity);
        GameObject enemy2 = Instantiate(allyPrefab, (transform.position - (transform.right * 72.5f) - (transform.up * 16)), Quaternion.identity);
        enemy2.GetComponent<EnemyAI>().Flip();
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
}
