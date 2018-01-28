using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSign : MonoBehaviour {

    public AI_Behaviour.TurnDirection dir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Escapee")
        {
            AI_Behaviour ai = collision.GetComponent<AI_Behaviour>();
            ai.SetTurnDirection(dir);
        }
    }
}
