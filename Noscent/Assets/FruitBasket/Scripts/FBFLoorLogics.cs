using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBFLoorLogics : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
                Debug.Log("Player missed");
                Destroy(collision.gameObject);
        }
    }
}
