using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBPlayerLogics : MonoBehaviour
{
    
    public FruitBasketLogics FruitBasketLogics;

    public float movementSpeed;
    Rigidbody2D rb;
    // Vector3 mousePos;
    // Vector2 position = new Vector2(0f, 0f);


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

        // mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        // mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // // mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, -4.29f, Camera.main.nearClipPlane));
        // position = Vector2.Lerp(transform.position, mousePos *30, 1f);
        // Debug.Log(mousePos);

        if (!paused)
        {    
            // rb.MovePosition(position);    
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

    // private void FixedUpdate()
    // {
    //     if (!paused)
    //     {
    //         rb.MovePosition(position);    
    //     }
    // }

}
