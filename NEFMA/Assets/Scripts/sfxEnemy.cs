using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxEnemy : MonoBehaviour {

    // Use this for initialization
    private EnemyAI ea;
    public AudioSource loafer_motion;
	void Start () {
        ea = GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ea.gameObject.GetComponent<Renderer>().isVisible)
        {
            if (ea.groundCheck == true && ea.isRanged == false && ea.ghostOverride == false && loafer_motion.isPlaying == false)
            {
                loafer_motion.Play();
            }
        }
	}
}
