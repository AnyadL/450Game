using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < Globals.players.Count; ++i)
        {
            if (Globals.players[i].GO == collision)
            {
                Globals.players[i].Score += 100;
                Debug.Log(Globals.players[i].Score);
                Destroy(gameObject);
            }
        }

    }
}
