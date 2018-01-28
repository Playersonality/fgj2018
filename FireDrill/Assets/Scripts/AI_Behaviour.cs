using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behaviour : MonoBehaviour {
    public enum TurnDirection
    {
        Left, Right
    }

    float normalSpeed = 0.06f;
    float speed = 0.0f;
    public float rotation = 0.0f;
    int turnDir = 1;
    Collider2D hitCollider;
    ParticleSystem fireParticles;
    LayerMask layerMask;
    Transform rend;
    Animator anim;
    bool nextToAWall = false;
    bool burning = false;
    bool escaped = false;
    float bonkDelay = 1.6f;
    float bonkTimer = 1.6f;


	// Use this for initialization
	void Start () {
        layerMask = 1 << 8 | 1 << 2; //npc | ignore raycast
        layerMask = ~layerMask;
        fireParticles = GetComponent<ParticleSystem>();
        rend = transform.GetChild(0);
        anim = transform.GetChild(0).GetComponent<Animator>();
        speed = normalSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (bonkTimer < bonkDelay)
        {
            bonkTimer += Time.fixedDeltaTime;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotation);
        rend.rotation = Quaternion.Euler(0, 0, 0);
        transform.Translate(Vector3.up * speed);

        Debug.DrawRay(transform.position, transform.up * 0.56f, Color.red);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.up, 0.56f, layerMask);

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
            Debug.DrawRay(transform.position, transform.right * 0.56f * (float)turnDir, Color.red);
            RaycastHit2D ray2 = Physics2D.Raycast(transform.position, transform.right * (float)turnDir, 0.56f, layerMask);

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
        nextToAWall = false;
        if (dir == TurnDirection.Left)
        {
            rotation += 90.0f;
            turnDir = 1;
        }
        else if (dir == TurnDirection.Right)
        {
            rotation -= 90.0f;
            turnDir = -1;
        }
    }

    public void SetOnFire()
    {
        burning = true;
        anim.SetBool("burning", true);
        normalSpeed *= 2.0f;
        speed = normalSpeed;
        fireParticles.Play();
        Destroy(this.gameObject, 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Escapee" &&
            bonkTimer >= bonkDelay &&
            !escaped &&
            !burning)
        {
            Debug.Log("Crashed to another person");
            Bonk();
            bonkTimer = 0.0f;
        }
    }

    private void Escape()
    {
        anim.SetBool("escaped", true);
        speed = 0.0f;
        Destroy(this.gameObject, 2.0f);
    }

    private void Bonk()
    {
        speed = -0.01f;
        Invoke("RecoverFromBonk", 0.6f);
    }

    private void RecoverFromBonk()
    {
        speed = normalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!escaped && !burning && collision.tag == "Exit")
        {
            Debug.Log("ESCAPED");
            Escape();
        }
        else if (!escaped && !burning && collision.tag == "Fire")
        {
            Debug.Log("walked into fire");
            SetOnFire();
        }
    }
}
