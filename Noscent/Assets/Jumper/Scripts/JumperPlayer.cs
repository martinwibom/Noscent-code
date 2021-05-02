using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlayer : MonoBehaviour
{   
    public JumperLogics Logics;
    Rigidbody2D rb;
    float jumpHeight = 12f;

    public GameObject smellWave;
    public GameObject nose;
    
    public bool paused;
    public bool smelling;
    bool leftLane;
    bool middleLane;
    bool rightLane;

    Vector2 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paused = true;
        startPos = transform.position;
        middleLane = true;
    }

    public void FreezePlayer()
    {
        rb.velocity = new Vector2(0,0);
        rb.isKinematic = true;
        paused = true;
    }

    public void UnfreezePlayer()
    {
        rb.isKinematic = false;
        paused = false;
    }

    void allLaneFalse()
    {
        middleLane = false;
        rightLane = false;
        leftLane = false;
    }

    IEnumerator PlayerSmelling()
    {
        Debug.Log("PlayerSmelling started");
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if(smelling) SpawnSmellWave();
        }
    }

    void SpawnSmellWave()
    {
        GameObject score = Instantiate(smellWave, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        score.transform.SetParent(nose.transform);
    }

    void Update()
    {
        if(!paused)
        {
            if(this.transform.position.y <= startPos.y + 0.2f)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) ||Input.GetKeyDown(KeyCode.UpArrow)) rb.velocity = new Vector2(0.0f, 1f * jumpHeight);
                if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.A) ) if(leftLane || middleLane) transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D)) if(rightLane || middleLane) transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            }

            if(transform.position.x > -0.1f && transform.position.x < 0.1f)
            {
                allLaneFalse();
                middleLane = true;
                Logics.UpdatePlayerPosition(1);
            } else if (transform.position.x > -1.1f && transform.position.x < -0.9f)
            {
                allLaneFalse();
                leftLane = true;
                Logics.UpdatePlayerPosition(0);
            } else if (transform.position.x > 0.9f && transform.position.x < 1.1f)
            {
                allLaneFalse();
                rightLane = true;
                Logics.UpdatePlayerPosition(2);
            }
        }
    }
}
