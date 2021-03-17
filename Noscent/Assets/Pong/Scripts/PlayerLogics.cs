using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogics : MonoBehaviour
{

    public float movementSpeed = 10f;
    Rigidbody2D rb;

    public bool paused = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        paused = true;
    }

    // Update is called once per frame
    void Update()
    {

        // var movement = Input.GetAxis("Vertical");
        // transform.position += new Vector3(0, movement, 0) * Time.deltaTime * movementSpeed;
        // // rb.velocity = new Vector2(movement.x, movement.y);

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
