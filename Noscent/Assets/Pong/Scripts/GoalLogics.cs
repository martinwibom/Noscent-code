using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLogics : MonoBehaviour
{
    public PongLogics PongLogics;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if(this.tag == "Goal")
            {
                Debug.Log("Player scored");
                PongLogics.Goal();
                PongLogics.Restart();
            } else if (this.tag == "RightWall")
            {
                Debug.Log("Player missed the ball, point deducted");
                PongLogics.Miss();
                PongLogics.Restart();

            }
        }
    }


}