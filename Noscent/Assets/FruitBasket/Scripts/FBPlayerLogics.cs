using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBPlayerLogics : MonoBehaviour
{
    
    public FruitBasketLogics FruitBasketLogics;

    public float movementSpeed = 10f;
    Rigidbody2D rb;


    public bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        paused = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
                Debug.Log("Player scored");
                FruitBasketLogics.Goal();
                Destroy(collision.gameObject);
        }
    }

    public void FreezePlayer()
    {
        rb.velocity = new Vector2(0,0);
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

        if (!paused)
        {    
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(1f * movementSpeed, 0.0f);

            } else if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(1f * movementSpeed, 0.0f);
            
            } else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-1f * movementSpeed, 0.0f);

            } else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-1f * movementSpeed, 0.0f);

            } else 
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
            }
        }

    }

}
