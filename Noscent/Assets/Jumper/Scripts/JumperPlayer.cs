using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private float jumpHeight = 10f;
    
    public bool paused;
    bool leftLane;
    bool middleLane;
    bool rightLane;

    Vector2 startPos;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        paused = true;
        startPos = this.transform.position;
        middleLane = true;
    }

    public void FreezePlayer()
    {
        rb.velocity = new Vector2(0,0);
        rb.isKinematic = true;
        paused = true;
    }

    public void UnfreezePlayer()
    {
        rb.isKinematic = false;
        paused = false;
    }

    void allLaneFalse()
    {
        middleLane = false;
        rightLane = false;
        leftLane = false;
    }


    void Update()
    {
        if(!paused)
        {
            if(this.transform.position.y <= startPos.y + 0.2f)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) ||Input.GetKeyDown(KeyCode.UpArrow)) rb.velocity = new Vector2(0.0f, 1f * jumpHeight);
                if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.A) ) if(leftLane || middleLane) transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D)) if(rightLane || middleLane) transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            }

            if(transform.position.x == 0)
            {
                allLaneFalse();
                middleLane = true;
            } else if (transform.position.x == -2)
            {
                allLaneFalse();
                leftLane = true;
            } else if (transform.position.x == 2)
            {
                allLaneFalse();
                rightLane = true;
            }
        }
    }
}
