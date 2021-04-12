using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogics : MonoBehaviour
{

    public float movementSpeed = 10f;
    Rigidbody2D rb;

    public bool paused = true;
    
    Vector3 startPosition;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        paused = true;
        startPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        rb.velocity = new Vector2(0,0);
    }

    void Update()
    {

        //Moves the player with velocity instead of transform.position
        //Player doesn't try and clip through the wall with this method. 

        if (!paused){    
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(0.0f, 1f * movementSpeed);

            } else if (Input.GetKey(KeyCode.W))
            {
                rb.velocity = new Vector2(0.0f, 1f * movementSpeed);
            
            } else if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = new Vector2(0.0f, -1f * movementSpeed);

            } else if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(0.0f, -1f * movementSpeed);

            } else 
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
            }
        }

    }
}
