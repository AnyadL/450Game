using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public GameObject myHead;
    public GameObject myLeftHand;
    public GameObject myRightHand;
    private int parts = 0;
    public int numberOfParts = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (parts == numberOfParts)
  //      {
  //          Debug.Log("Linked");
  //      }
  //      else
  //      {
  //          Debug.Log("Not Linked");
  //      }
	}

    public void registerBodyPart(GameObject part, int num)
    {
        Debug.Log("registerBodyPart called with num = " + num);
        if (num == -1)
        {
            if (myLeftHand == null)
            {
                myLeftHand = part;
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
