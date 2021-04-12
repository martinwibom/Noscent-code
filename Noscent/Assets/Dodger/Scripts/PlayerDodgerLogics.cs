using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgerLogics : MonoBehaviour
{

    public DodgerLogics Logics;

    public bool paused;

    Rigidbody2D rb;
    
    Vector3 middleLane = new Vector3(0,-2.75f,0);
    Vector3 rightLane = new Vector3(2, -2.75f, 0);
    Vector3 leftLane = new Vector3(-2, -2.75f, 0);


    void Start()
    {
        paused = true;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(middleLane);
        Debug.Log(transform.position);
        // rb.velocity = new Vector2(0,0.1f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Player hit");
        if(other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Player hit");
            Logics.StartCoroutine("PlayerHit");
        } else if(other.gameObject.CompareTag("Goal"))
        {
            Logics.GameOver();
        }
    }

    public void FreezePlayer()
    {
        paused = true;
        rb.velocity = new Vector2(0,0);
    }

    public void UnfreezePlayer()
    {
        paused = false;
        rb.velocity = new Vector2(0,0.1f);
    }

    void Goal()
    {
        if(transform.position == Logics.goal.transform.position)
        {
            paused = true;
            Logics.GameOver();
        }
    }


    void Update()
    {
        if(!paused)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(transform.position.x == 0f || transform.position.x == 2f)
                transform.position += new Vector3(-2, 0, 0);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(transform.position.x == 0f || transform.position.x == -2f)
                transform.position += new Vector3(2, 0, 0);
            }
        }

    }
}
