using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogics : MonoBehaviour
{

    float speed = 10f;
    public Rigidbody2D rb;

    Vector2 ballVelocity;

    Vector3 startPosition;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        // Launch();
    }

    void FixedUpdate() 
    {  
        if(rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    public void Pause()
    {
        ballVelocity = rb.velocity;
        rb.velocity = new Vector2(0,0);
    }

    public void Resume()
    {
        rb.velocity = ballVelocity;
    }
    
    //Resets the ball to it's original position with zero speed
    public void ResetBall()
    {
        transform.position = startPosition;
        rb.velocity = new Vector2(0,0);
    }

    //Sets the ball at a random Y position
    public void SetRandomPosition()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-3.2f, 3.2f), 0);
    }

    //Launches the ball into a random Y direction towards the player
    public void Launch()
    {
        // float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.velocity = new Vector2(speed, speed * y);

        Debug.Log("IT RAN");

    }
}
