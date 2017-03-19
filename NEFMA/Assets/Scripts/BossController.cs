using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [HideInInspector] public GameObject myHead;
    [HideInInspector] public GameObject myLeftHand;
    [HideInInspector] public GameObject myRightHand;
    [HideInInspector] public BossHead myHeadScript;
    [HideInInspector] public BossHand myLeftHandScript;
    [HideInInspector] public BossHand myRightHandScript;
    public GameObject allyPrefab;
    private int parts = 0;
    public int numberOfParts = 3;
    [HideInInspector] public float lowerHands = 0;
    [HideInInspector] public float nextSwipe;
    public float swipeCooldown = 10f;
    [HideInInspector] public float nextFireball;
    public float fireballCooldown = 15f;
    [HideInInspector] public float nextSpawn;
    public float spawnCooldown = 15f;

    // Use this for initialization
    void Start () {
        nextSwipe = Time.time + swipeCooldown;
        nextFireball = Time.time + fireballCooldown;
        nextSpawn = Time.time + spawnCooldown;
    }
	
	// Update is called once per frame
	void Update () {
        if (lowerHands <= Time.time && lowerHands != 0)
        {
            myLeftHandScript.resetHands(0);
            myRightHandScript.resetHands(1);
            lowerHands = 0;
        }
        if (nextSwipe <= Time.time)
        {
            myLeftHandScript.raiseHand(myLeftHandScript.maxHeight);
            myRightHandScript.raiseHand(myLeftHandScript.maxHeight);
            nextSwipe = Time.time + swipeCooldown;
            lowerHands = Time.time + 2;
        }
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
        Instantiate(allyPrefab, (transform.position + (transform.right * 57) - (transform.up * 11)), Quaternion.identity);
        GameObject enemy2 = Instantiate(allyPrefab, (transform.position - (transform.right * 57) - (transform.up * 11)), Quaternion.identity);
        enemy2.GetComponent<EnemyAI>().Flip();
    }

    public void registerBodyPart(GameObject part, int num)
    {
        Debug.Log("registerBodyPart called with num = " + num);
        if (num == -1)
        {
            if (myLeftHand == null)
            {
                myLeftHand = part;
                myLeftHandScript = myLeftHand.GetComponent<BossHand>();
            }
            else
            {
                Debug.Log("Bad body part number given to registerBodyPart (Received num = -1) (Already recieved -1)");
                return;
            }
        }
        else if (num == 0)
        {
            if (myHead == null)
            {
                myHead = part;
                myHeadScript = myHead.GetComponent<BossHead>();
            }
            else
            {
                Debug.Log("Bad body part number given to registerBodyPart (Received num = 0) (Already recieved 0)");
                return;
            }
        }
        else if (num == 1)
        {
            if (myRightHand == null)
            {
                myRightHand = part;
                myRightHandScript = myRightHand.GetComponent<BossHand>();
            }
            else
            {
                Debug.Log("Bad body part number given to registerBodyPart (Received num = 1) (Already recieved 1)");
                return;
            }
        }
        else
        {
            Debug.Log("Bad body part number given to registerBodyPart (Received num = " + num + ")");
            return;
        }
        parts++;
    }
}
