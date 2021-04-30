using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperObjects : MonoBehaviour
{

    Rigidbody2D rb;
    JumperLogics Logics;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        Logics = GameObject.Find("Logics").GetComponent<JumperLogics>();
        SetSpeed(-3f);
    }


    public void SetSpeed(float speed)
    {
        rb.velocity = new Vector2(0,speed);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player scored!");
            Logics.AddScore();
            Destroy(this.gameObject);
        } else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
