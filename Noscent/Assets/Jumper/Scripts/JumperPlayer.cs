﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private float jumpHeight = 10f;
    
    public bool paused;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        paused = true;
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


    void Update()
    {
        if(!paused)
        {
            if(this.transform.position.y <= -3.3f)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        rb.velocity = new Vector2(0.0f, 1f * jumpHeight);

                    } else if (Input.GetKeyDown(KeyCode.W))
                    {
                        rb.velocity = new Vector2(0.0f, 1f * jumpHeight);
                    
                    } else if (Input.GetKeyDown(KeyCode.Space))
                    {
                        rb.velocity = new Vector2(0.0f, 1f * jumpHeight);
                    
                    }
            }
        }
    }
}
