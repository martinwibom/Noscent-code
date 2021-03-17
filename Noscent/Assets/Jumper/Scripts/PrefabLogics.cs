using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLogics : MonoBehaviour
{
    Rigidbody2D rb;

    JumperLogics Logics;

    // Start is called before the first frame update
    void Start()
    {
        Logics = GameObject.Find("Logics").GetComponent<JumperLogics>();
        float speed = Logics.prefabSpeed;
        rb = this.GetComponent<Rigidbody2D>();  
        rb.velocity = new Vector2(speed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player lost");
        } else if (collision.gameObject.CompareTag("Goal"))
        {
            Destroy(this.gameObject);
        }
    }
}
