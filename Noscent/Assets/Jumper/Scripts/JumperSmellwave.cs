using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperSmellwave : MonoBehaviour
{
    JumperLogics Logics;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Logics = GameObject.Find("Logics").GetComponent<JumperLogics>();
        Move();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Goal"))
        {
            Logics.AddScore();
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        rb.velocity = new Vector2(0,5f);
    }
    public void Freeze()
    {
        rb.velocity = new Vector2(0,0f);
    }

}
