using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour {

    //private Rigidbody2D myBody;
    //private AttributeController myAttributes;
    private EnemyAI myAI;

    private GameObject target;
    public float xRange = 20f;
    public float yRange = 10f;
    private List<GameObject> targets = new List<GameObject>();
    public float teleportCooldown = 5f;
    private float nextTeleport = 0f;

    public GameObject TEST;

    // Use this for initialization
    void Start () {
        //myBody = this.GetComponent<Rigidbody2D>();
        //myAttributes = this.GetComponent<AttributeController>();
        myAI = this.GetComponent<EnemyAI>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<Renderer>().isVisible)
        {
            return;
        }
        if (nextTeleport > Time.time)
        {
            if (target != null)
            {
                chase();
            }
            return;
        }
        else if (target != null)
        {
            target = null;
            myAI.ghostOverride = false;
        }

        if (Globals.player1.Alive)
        {
            targeter(Globals.player1.GO);
        }
        if (Globals.player2.Alive)
        {
            targeter(Globals.player2.GO);
        }
        if (Globals.player3.Alive)
        {
            targeter(Globals.player3.GO);
        }
        if (Globals.player4.Alive)
        {
            targeter(Globals.player4.GO);
        }
        if (TEST != null)
        {
            targeter(TEST);
        }

        //Debug.Log("Targets: " + targets.Count);
        if (targets.Count > 0)
        {
            teleport();
        }
    }

    void targeter(GameObject target)
    {
        //Debug.Log("Viewing " + target);
        float tx = target.transform.position.x;
        float ty = target.transform.position.y;
        float mx = gameObject.transform.position.x;
        float my = gameObject.transform.position.y;
        if (Mathf.Abs(mx - tx) <= xRange && Mathf.Abs(my - ty) <= yRange)
        {
            //Debug.Log("Considering " + target);
            targets.Add(target);
        }
    }

    void teleport()
    {
        int choice = Random.Range(0, targets.Count);
        target = targets[choice];
        bool dir = target.GetComponent<HeroMovement>().facingRight;
        //Debug.Log("Chosen " + targets[choice]);
        gameObject.transform.position = target.transform.position - new Vector3((dir? 1.5f : -1.5f), -0.25f, 0);
        if ((myAI.facingRight == -1 && dir) || (myAI.facingRight == 1 && !dir))
        {
            myAI.Flip();
        }
        myAI.ghostOverride = true;
        nextTeleport = Time.time + teleportCooldown;
        targets.Clear();
    }

    void chase()
    {
        float tx = target.transform.position.x;
        float mx = gameObject.transform.position.x;
        if ((tx > mx && myAI.facingRight == -1) || (tx < mx && myAI.facingRight == 1))
        {
            myAI.Flip();
        }
    }
}
