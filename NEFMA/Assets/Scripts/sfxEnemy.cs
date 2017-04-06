using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxEnemy : MonoBehaviour {

    // Use this for initialization
    private EnemyAI ea;
    //private GhostAI ga;
    public AudioSource loafer_motion;
    public AudioSource frog_attack;
	void Start () {
        ea = GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ea.gameObject.GetComponent<Renderer>().isVisible)
        {
            if (ea.isRanged == false && ea.ghostOverride == false)  //If enemy is a loafer
            {
                if (ea.groundCheck == true && loafer_motion.isPlaying == false && loafer_motion != null)
                {
                    loafer_motion.pitch = Random.Range(1f,1.5f);
                    loafer_motion.Play();
                }
            }
            else if (ea.isRanged == true) //If enemy is a froggy
            {
 
            }
            else if (ea.ghostOverride == true) //If ghost
            {
                //ga = GetComponent<GhostAI>();
                
            }
        }
	}
}
