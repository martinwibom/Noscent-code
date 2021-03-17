using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogics : MonoBehaviour
{

    public float speed = 5f;
    Rigidbody2D rb;

    new Vector2 ballVelocity;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        // Launch();
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

    //Launches the ball into a random Y direction towards the player from a random Y position.
    public void Launch()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(-3.2f, 3.2f), 0);
        

        // float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.velocity = new Vector2(speed * 1, speed * y);

    }
}
