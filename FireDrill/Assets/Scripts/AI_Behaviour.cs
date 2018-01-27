using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behaviour : MonoBehaviour {
    public enum TurnDirection
    {
        Left, Right
    }

    public float speed = 0.02f;
    public float rotation = 0.0f;
    int turnDir = 1;
    Collider2D hitCollider;
    LayerMask layerMask;
    bool nextToAWall = false;


	// Use this for initialization
	void Start () {
        layerMask = 1 << 8 | 1 << 2; //npc | ignore raycast
        layerMask = ~layerMask;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        transform.Translate(Vector3.up * speed);

        Debug.DrawRay(transform.position, transform.up * 0.24f, Color.red);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.up, 0.24f, layerMask);

        if (ray.collider != null)
        {
            if (ray.collider.gameObject.tag == "Wall")
            {
                Debug.Log("Hit a wall!");
                rotation += 90.0f * turnDir;
                nextToAWall = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.right * 0.24f * (float)turnDir, Color.red);
            RaycastHit2D ray2 = Physics2D.Raycast(transform.position, transform.right * (float)turnDir, 0.24f, layerMask);

            if (nextToAWall && ray2.collider == null)
            {
                Debug.Log("Moved away from a wall!");
                rotation -= 90.0f * turnDir;
                nextToAWall = false;
            }
            else if (!nextToAWall && ray2.collider != null)
            {
                nextToAWall = true;
            }
        }
    }

    public void SetTurnDirection(TurnDirection dir)
    {
        if (dir == TurnDirection.Left)
        {
            turnDir = 1;
        }
        else if (dir == TurnDirection.Right)
        {
            turnDir = -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {

        }
    }
}
