using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPrefabLogics : MonoBehaviour
{
    Rigidbody2D rb;

    JumperLogics Logics;
    
    float speed;

    public bool frozen;

    // Start is called before the first frame update
    void Start()
    {
        Logics = GameObject.Find("Logics").GetComponent<JumperLogics>();
        speed = Logics.prefabSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        frozen = false;
        // rb.velocity = new Vector2(speed, 0f);
        // transform.position = new Vector3(-3f, 0f, 0f);
    }

    private void Update() {
        // transform.position += new Vector3(-10f,0f,0) * Time.deltaTime;
        if(!frozen)
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player lost");
            Logics.StartCoroutine("ObstacleHit");
        } else if (collision.gameObject.CompareTag("Goal"))
        {
            Destroy(this.gameObject);
            Logics.AddScore();
        }
    }
}
