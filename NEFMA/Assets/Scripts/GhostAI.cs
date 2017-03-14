using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour {

    //private Rigidbody2D myBody;
    //private AttributeController myAttributes;
    private EnemyAI myAI;

    private GameObject target;
    public float xRange = 0f;
    public float yRange = 0f;
    private List<GameObject> targets = new List<GameObject>();
    public float teleportCooldown = 5f;
    private float nextTeleport = 0f;
    public float fadeDuration = 1f;
    private float fadeTime = 0f;
    private int fade = 0;

    public GameObject TEST;

    // Use this for initialization
    void Start () {
        //myBody = this.GetComponent<Rigidbody2D>();
        //myAttributes = this.GetComponent<AttributeController>();
        myAI = this.GetComponent<EnemyAI>();
    }
	
	// Update is called once per frame
	void Update () {
        if (fadeTime >= Time.time)
        {
            if (fade == -1)
            {
                //Debug.Log("Time: " + Mathf.Abs(Time.time - fadeTime) + " Sin(" + 0.5f * Mathf.PI * Mathf.Abs(Time.time - fadeTime) + ") = " + Mathf.Sin(0.5f * Mathf.PI * Mathf.Abs(Time.time - fadeTime)));
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Sin(0.5f * Mathf.PI * Mathf.Abs(Time.time - fadeTime)));
            }
            else if (fade == 1)
            {
                //Debug.Log("Time: " + Mathf.Abs(Time.time - fadeTime) + " Cos(" + 0.5f * Mathf.PI * Mathf.Abs(Time.time - fadeTime) + ") = " + Mathf.Cos(0.5f * Mathf.PI * Mathf.Abs(Time.time - fadeTime)));
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Cos(0.5f * Mathf.PI * Mathf.Abs(Time.time - fadeTime)));
            }
            return;
        }
        if (fade == -1)
        {
            teleport();
            return;
        }
        else if (fade == 1)
        {
            //myAI.currentMoveForce = myAI.moveForce;
            fade = 0;
        }
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

        Debug.DrawLine(transform.position + (Vector3.right * xRange) + (Vector3.up * yRange), transform.position - (Vector3.right * xRange) + (Vector3.up * yRange), Color.red, 0.1f);
        Debug.DrawLine(transform.position + (Vector3.right * xRange) + (Vector3.up * yRange), transform.position + (Vector3.right * xRange) - (Vector3.up * yRange), Color.red, 0.1f);
        Debug.DrawLine(transform.position - (Vector3.right * xRange) + (Vector3.up * yRange), transform.position - (Vector3.right * xRange) - (Vector3.up * yRange), Color.red, 0.1f);
        Debug.DrawLine(transform.position + (Vector3.right * xRange) - (Vector3.up * yRange), transform.position - (Vector3.right * xRange) - (Vector3.up * yRange), Color.red, 0.1f);
        for (int i = 0; i < Globals.players.Count; i++)
        {
            if (Globals.players[i].Alive)
            {
                targeter(Globals.players[i].GO);
            }
        }

        if (TEST != null)
        {
            targeter(TEST);
        }

        //Debug.Log("Targets: " + targets.Count);
        if (targets.Count > 0)
        {
            choose();
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

    void choose()
    {
        int choice = Random.Range(0, targets.Count);
        target = targets[choice];
        targets.Clear();
        myAI.ghostOverride = true;
        //myAI.currentMoveForce = 0;
        nextTeleport = Time.time + teleportCooldown;
        fadeTime = Time.time + fadeDuration;
        fade = -1;
    }

    void teleport()
    {
        
        bool dir = target.GetComponent<HeroMovement>().facingRight;
        //Debug.Log("Chosen " + targets[choice]);
        gameObject.transform.position = target.transform.position - new Vector3((dir? 1.5f : -1.5f), -0.25f, 0);
        if ((myAI.facingRight == -1 && dir) || (myAI.facingRight == 1 && !dir))
        {
            myAI.Flip();
        }
        fadeTime = Time.time + fadeDuration;
        fade = 1;
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
