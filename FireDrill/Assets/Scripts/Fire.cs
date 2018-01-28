using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    enum FireDirection
    {
        Left, Right, Up, Down, None
    }

    float spreadTime = 0.16f;
    float spreadTimer = 0.0f;
    public Transform flame;
    List<Transform> flames;
    public Vector3 startPos;
    BoxCollider2D selfCollider;

	// Use this for initialization
	void Start () {
        flames = new List<Transform>();
        Transform obj = Instantiate(flame);
        obj.transform.position = startPos;
        flames.Add(obj);
        selfCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        spreadTimer += Time.fixedDeltaTime;
        if (spreadTimer >= spreadTime)
        {
            spreadTimer = 0.0f;
            SpreadFire();
        }
    }

    private void SpreadFire()
    {
        int flamesCount = flames.Count;
        int index = 0;
        FireDirection freeDir = FireDirection.None;
        bool looping = true;
        while (looping)
        {
            flamesCount = flames.Count;
            index = Random.Range(0, flamesCount);
            freeDir = GetFreeDirection(flames[index]);
            if (freeDir != FireDirection.None)
            {
                looping = false;
            }
            else
            {
                flames.RemoveAt(index);
            }
        }

        Transform obj = Instantiate(flame);
        obj.transform.position = flames[index].transform.position + GetSpreadOffset(freeDir);
        flames.Add(obj);
    }

    private FireDirection GetFreeDirection(Transform obj)
    {
        int startDir = Random.Range(0, (int)FireDirection.None);
        int currentDir = (startDir + 1) % (int)FireDirection.None;
        bool looped = false;
        while (!looped)
        {
            Vector2 rayDir = RayDirection((FireDirection)currentDir);
            RaycastHit2D[] hits = Physics2D.RaycastAll(obj.transform.position + new Vector3(rayDir.x, rayDir.y) * 1.0f, RayDirection((FireDirection)currentDir), 0.05f);
            //Debug.DrawRay(obj.transform.position + new Vector3(rayDir.x, rayDir.y) * 1.0f, RayDirection((FireDirection)currentDir) * 0.05f);
            bool flameHit = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != selfCollider &&
                    hit.collider.tag == "Fire")
                {
                    flameHit = true;
                    break;
                }
            }

            if (!flameHit)
            {
                return (FireDirection)currentDir;
            }

            if (currentDir == startDir)
            {
                looped = true;
            }

            currentDir = (currentDir + 1) % (int)FireDirection.None;
        }

        return FireDirection.None;
    }

    private Vector2 RayDirection(FireDirection dir)
    {
        switch (dir)
        {
            case FireDirection.Up:
                return Vector2.down;
                break;

            case FireDirection.Down:
                return Vector2.up;
                break;

            case FireDirection.Left:
                return Vector2.left;
                break;

            case FireDirection.Right:
                return Vector2.right;
                break;

            default:
                return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    private Vector3 GetSpreadOffset(FireDirection dir)
    {
        switch (dir)
        {
            case FireDirection.Up:
                return new Vector3(0.0f, -1.0f, 0.0f);
                break;

            case FireDirection.Down:
                return new Vector3(0.0f, 1.0f, 0.0f);
                break;

            case FireDirection.Left:
                return new Vector3(-1.0f, 0.0f, 0.0f);
                break;

            case FireDirection.Right:
                return new Vector3(1.0f, 0.0f, 0.0f);
                break;

            default:
                return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }


}
