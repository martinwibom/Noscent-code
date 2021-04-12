using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDodger : MonoBehaviour
{

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -3f);
    }

    void Update()
    {
        if(transform.position.y < -4.5f)
            Destroy(this.gameObject);
    }
}
