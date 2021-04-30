using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPrefabLogics : MonoBehaviour
{
    Rigidbody2D rb;

    JumperLogics Logics;
    
    float speed;
    public bool movingRight;

    public bool frozen;
    bool hit;

    void Start()
    {
        Logics = GameObject.Find("Logics").GetComponent<JumperLogics>();
        speed = Logics.prefabSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        frozen = false;
        hit = false;
        if(movingRight) speed = speed * -1;

    }

    private void Update() {
        if(!frozen) transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !hit)
        {
            Debug.Log("Player lost");
            Logics.StartCoroutine("ObstacleHit");
            hit = true;
        } else if (collision.gameObject.CompareTag("Goal"))
        {
            // if(!hit) Logics.AddScore();
            Destroy(this.gameObject);
        }
    }
}
