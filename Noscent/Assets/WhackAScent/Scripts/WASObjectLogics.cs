using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASObjectLogics : MonoBehaviour
{
    WASLogics Logics;
    Rigidbody2D rb;

    Vector3 moveTo;

    public int myNumber;

    void Start() 
    {   
        Logics = GameObject.Find("Logics").GetComponent<WASLogics>();
        Vector3 moveTo = this.transform.position + new Vector3(0, .5f, 0);
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {     
        rb.velocity = new Vector2(0f, 1f);

        yield return new WaitForSeconds(1f);

        rb.velocity = new Vector2(0f, 0f);

        yield break;
    }

    IEnumerator Destroyed()
    {
        rb.velocity = new Vector2(0f, -1f);

        yield return new WaitForSeconds(1f);

        rb.velocity = new Vector2(0f, 0f);

        Logics.spawnPointOccupied[myNumber] = false;
        Logics.usedAmount--;
        Destroy  (this.gameObject);    

    }

    private void OnMouseDown()
    {
        bool clicked = false;
        if(!clicked)
        {
            StartCoroutine("Destroyed");
            clicked = true;
        }
    }
}
